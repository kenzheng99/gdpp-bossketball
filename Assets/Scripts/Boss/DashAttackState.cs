using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "BossStates/DashAttackState")]
public class DashAttackState : BossState {

    [SerializeField] private float durationSeconds;
    // [SerializeField] private float waitTimeSeconds;
    // [SerializeField] private Bounds moveBounds;
    [SerializeField] private float directionTimeSeconds;
    // [SerializeField] private float moveDirectionDelta;
    [SerializeField] private float moveSpeed;
    [SerializeField] 
    
    private GameObject boss;
    private Transform playerTr;
    private Timer stateTimer;
    private Timer directionTimer;

    private Vector3 moveDir;
    private float dirDelta;
    private int stage;
    
    public override void EnterState(BossStateMachine stateMachine) {
        Debug.Log("DashAttackState");
        stateTimer = new Timer(durationSeconds);
        directionTimer = new Timer(directionTimeSeconds);
        boss = stateMachine.gameObject;
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;

        moveDir = boss.transform.position - playerTr.position;
        stage = 1;
    }

    public override void UpdateState(BossStateMachine stateMachine) {

        switch (stage) {
            case 1:
                moveDir = playerTr.position - boss.transform.position;
                moveDir.y += 15;
                boss.transform.Translate(moveSpeed/2 * Time.deltaTime * moveDir);

                if (Vector3.Distance(boss.transform.position, playerTr.position) < 10) ;
                break;
            default:
                break;
        }

        // if (directionTimer.Done()) {
        //     dirDelta = Random.Range(-moveDirectionDelta, moveDirectionDelta);
        //     directionTimer = new Timer(waitTimeSeconds);
        // }
        //
        // var bossPos = boss.transform.position;
        // if (Math.Abs(bossPos.x) > moveBounds.max.x) {
        //     moveDir.x *= -1;
        // } else if (bossPos.y > moveBounds.max.y || bossPos.y < moveBounds.min.y) {
        //     moveDir.y *= -1;
        // }
        //
        // moveDir = Quaternion.Euler(0, 0, dirDelta) * moveDir;
        // boss.transform.Translate(moveSpeed * Time.deltaTime * moveDir);
        // directionTimer.Tick(Time.deltaTime);
        //
        // // end state if timer done
        // stateTimer.Tick(Time.deltaTime);
        // if (stateTimer.Done()) {
        //     stateMachine.SwitchToRandomState();
        // }
    }

    // private Vector2 PickNewTargetPosition() {
    //     float targetX = Random.Range(moveBounds.min.x, moveBounds.max.x);
    //     float targetY = Random.Range(moveBounds.min.y, moveBounds.max.y);
    //     return new Vector3(targetX, targetY);
    // }
}
