using UnityEngine;

[CreateAssetMenu(menuName = "BossStates/IdleState")]
public class IdleState : BossState {

    [SerializeField] private float durationSeconds;
    [SerializeField] private float moveTime;
    [SerializeField] private float waitTimeSeconds;
    [SerializeField] private Bounds moveBounds;
    
    private GameObject boss;
    private Timer stateTimer;
    private Timer waitTimer;
    private Vector2 targetPosition;
    private bool arrivedAtTargetPosition;
    private Vector2 currentVelocity;
    public override void EnterState(BossStateMachine stateMachine) {
        Debug.Log("IdleState");
        stateTimer = new Timer(durationSeconds);
        waitTimer = new Timer(waitTimeSeconds);
        boss = stateMachine.gameObject;
        
        targetPosition = PickNewTargetPosition();
        arrivedAtTargetPosition = false;
    }

    public override void UpdateState(BossStateMachine stateMachine) {

        if (arrivedAtTargetPosition) {
            // waiting at target position
            //bossRb.velocity = Vector2.zero;
            waitTimer.Tick(Time.deltaTime);
            if (waitTimer.Done()) {
                targetPosition = PickNewTargetPosition();
                arrivedAtTargetPosition = false;
            }
        } else if (Vector2.Distance(boss.transform.position, targetPosition) < 0.01) {
            // just arrived at target position
            arrivedAtTargetPosition = true;
            waitTimer = new Timer(waitTimeSeconds);
        } else {
            // move towards target position
            // linear movement
            //Vector2 newPosition = Vector2.MoveTowards(boss.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            
            // smooth movement
            Vector2 newPosition = Vector2.SmoothDamp(
                boss.transform.position,
                targetPosition,
                ref currentVelocity,
                moveTime
            );
            boss.transform.position = newPosition;
        }
        
        // end state if timer done
        stateTimer.Tick(Time.deltaTime);
        if (stateTimer.Done()) {
            stateMachine.SwitchState(stateMachine.spiralAttackState);
        }
    }

    private Vector2 PickNewTargetPosition() {
        float targetX = Random.Range(moveBounds.min.x, moveBounds.max.x);
        float targetY = Random.Range(moveBounds.min.y, moveBounds.max.y);
        return new Vector3(targetX, targetY);
    }
}
