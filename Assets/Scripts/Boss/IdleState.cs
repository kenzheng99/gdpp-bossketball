using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "BossStates/IdleState")]
public class IdleState : BossState {

    [SerializeField] private float durationSeconds;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float waitTimeSeconds;
    [SerializeField] private Bounds moveBounds;
    
    private Rigidbody2D bossRb;
    private Timer stateTimer;
    private Timer waitTimer;
    private Vector2 targetPosition;
    private bool arrivedAtTargetPosition;
    public override void EnterState(BossStateMachine stateMachine) {
        Debug.Log("IdleState");
        stateTimer = new Timer(durationSeconds);
        waitTimer = new Timer(waitTimeSeconds);
        bossRb = stateMachine.GetComponent<Rigidbody2D>();
        
        targetPosition = PickNewTargetPosition();
        arrivedAtTargetPosition = false;
    }

    public override void UpdateState(BossStateMachine stateMachine) {

        if (arrivedAtTargetPosition) {
            // waiting at target position
            bossRb.velocity = Vector2.zero;
            waitTimer.Tick(Time.deltaTime);
            if (waitTimer.Done()) {
                targetPosition = PickNewTargetPosition();
                arrivedAtTargetPosition = false;
            }
        } else if (Vector2.Distance(bossRb.position, targetPosition) < 1) {
            // just arrived at target position
            arrivedAtTargetPosition = true;
            waitTimer = new Timer(waitTimeSeconds);
        } else {
            // move towards target position
            Vector2 velocity = (targetPosition - bossRb.position).normalized * moveSpeed;
            bossRb.velocity = velocity;
            
            // this method was a bit less smooth than setting velocity directly
            // Vector2 newPosition = Vector2.MoveTowards(bossRb.position, targetPosition, moveSpeed * Time.deltaTime);
            // bossRb.MovePosition(newPosition);
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
