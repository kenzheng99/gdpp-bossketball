using UnityEngine;

[CreateAssetMenu(menuName = "BossStates/DashAttackState")]
public class DashAttackState : BossState {
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float aggroDistance;
    [SerializeField] private float shakeDurationSeconds;
    [SerializeField] private float shakeMagnitude;
    [SerializeField] private float shakeSpeed;
    [SerializeField] private float dashWaitSeconds;
    [SerializeField] private int numAttacks;
    [SerializeField] private int numDashes;
    [SerializeField] private float postAttackWaitSeconds;

    private GameObject boss;
    private Transform playerTr;
    private Timer stateTimer;
    private Timer directionTimer;
    private Timer shakeTimer;
    private Timer dashTimer;
    private Timer dashWaitTimer;
    private Timer postAttackWaitTimer;

    private Vector3 moveDir;
    private Vector3 playerPosHold;
    private bool newDash;
    private bool updateHoldPos;
    private float dirDelta;
    private int stage;
    private int attacks;
    private int dashes;

    public override void EnterState(BossStateMachine stateMachine) {
        Debug.Log("DashAttackState");

        SoundManager.Instance.PlayDashingAttackIntroSound();
        if (GameManager.Instance.bossPhaseTwo == true)
        {
            SoundManager.Instance.PlayBossGenericRoarSound();
        }
        postAttackWaitTimer = new Timer(postAttackWaitSeconds);
        boss = stateMachine.gameObject;
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;

        moveDir = boss.transform.position - playerTr.position;
        stage = 1;
        newDash = true;
        updateHoldPos = true;
        attacks = 0;
        dashes = 0;
    }

    public override void UpdateState(BossStateMachine stateMachine) {

        var playerPos = playerTr.position;
        var bossPos = boss.transform.position;
        
        switch (stage) {
            case 1:
                // stage one: go to player
                moveDir = playerPos - bossPos;
                moveDir.y += 15;
                boss.transform.Translate(moveSpeed * Time.deltaTime * moveDir);

                if (Vector3.Distance(bossPos, playerPos) < aggroDistance) {
                    stage = 2;
                }
                break;
            case 2:
                // stage 2: shake
                shakeTimer ??= new Timer(shakeDurationSeconds);
                shakeTimer.Tick(Time.deltaTime);
                
                var shakeX = bossPos.x + Mathf.Sin(Time.time * shakeSpeed) * shakeMagnitude;
                boss.transform.position = new Vector3(shakeX,bossPos.y,bossPos.z);
                // spawn arrow to indicate dash direction on screen

                if (updateHoldPos) {
                    // hold player position for first dash
                    playerPosHold =  playerPos;
                    updateHoldPos = false;
                }

                if (shakeTimer.Done()) {
                    shakeTimer = null;
                    stage = 3;
                }
                break;
            case 3:
                // stage 3: dash at player
                if (newDash) {
                    moveDir = playerPosHold - bossPos;
                    boss.transform.Translate(dashSpeed * Time.deltaTime * moveDir);
                }

                if (moveDir.magnitude < 0.5) {
                    // update player hold position once after dashing
                    if (newDash) {
                        playerPosHold = playerPos;
                    }
                    newDash = false;

                    dashWaitTimer ??= new Timer(dashWaitSeconds);
                    dashWaitTimer.Tick(Time.deltaTime);
                    
                    boss.transform.Translate(dashSpeed* 4 * Time.deltaTime * Vector3.up);
                    
                    if (dashWaitTimer.Done()) {
                        dashWaitTimer = null;
                        newDash = true;
                        dashes++;
                        if (dashes >= numDashes) {
                            dashes = 0;
                            attacks++;
                            stage = 4;
                        }
                    }
                }
                break;
            case 4:
                // finished dashing, repeat attack if needed
                if (attacks < numAttacks) {
                    updateHoldPos = true;
                    stage = 1;
                }
                else {
                    postAttackWaitTimer.Tick(Time.deltaTime);
                    if (postAttackWaitTimer.Done()) {
                        if (GameManager.Instance.bossPhaseTwo) {
                            stateMachine.SwitchState(stateMachine.spiralAttackStatePhaseTwo);
                        }
                        else {
                            stateMachine.SwitchState(stateMachine.spiralAttackState);
                        }
                    }
                }
                break;
            default:
                Debug.Log("Unknown stage of attack: " + stage);
                break;
        }
    }
}
