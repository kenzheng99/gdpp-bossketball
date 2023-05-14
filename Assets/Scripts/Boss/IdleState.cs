using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "BossStates/IdleState")]
public class IdleState : BossState {

    [SerializeField] private float durationSeconds;
    [SerializeField] private float waitTimeSeconds;
    [SerializeField] private Bounds moveBounds;
    [SerializeField] private float directionTimeSeconds;
    [SerializeField] private float moveDirectionDelta;
    [SerializeField] private float moveSpeed;
    
    private GameObject boss;
    private Timer stateTimer;
    private Timer directionTimer;

    private Vector3 moveDir;
    private float dirDelta;
    
    public override void EnterState(BossStateMachine stateMachine) {
        Debug.Log("IdleState");
        stateTimer = new Timer(durationSeconds);
        directionTimer = new Timer(directionTimeSeconds);
        boss = stateMachine.gameObject;
        
        moveDir = Vector3.down;
        dirDelta = moveDirectionDelta;
    }

    public override void UpdateState(BossStateMachine stateMachine) {

        if (directionTimer.Done()) {
            dirDelta = Random.Range(-moveDirectionDelta, moveDirectionDelta);
            directionTimer = new Timer(waitTimeSeconds);
        }

        var bossPos = boss.transform.position;
        if (Math.Abs(bossPos.x) > moveBounds.max.x) {
            moveDir.x *= -1;
        } else if (bossPos.y > moveBounds.max.y || bossPos.y < moveBounds.min.y) {
            moveDir.y *= -1;
        }
        
        moveDir = Quaternion.Euler(0, 0, dirDelta) * moveDir;
        boss.transform.Translate(moveSpeed * Time.deltaTime * moveDir);
        directionTimer.Tick(Time.deltaTime);
        
        // end state if timer done
        stateTimer.Tick(Time.deltaTime);
        if (stateTimer.Done()) {
            stateMachine.SwitchToRandomState();
        }
    }

    private Vector2 PickNewTargetPosition() {
        float targetX = Random.Range(moveBounds.min.x, moveBounds.max.x);
        float targetY = Random.Range(moveBounds.min.y, moveBounds.max.y);
        return new Vector3(targetX, targetY);
    }
}
