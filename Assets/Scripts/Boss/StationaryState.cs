using UnityEngine;
using UnityEngine;

[CreateAssetMenu(menuName = "BossStates/StationaryState")]
public class StationaryState : BossState {

    [SerializeField] private float bossMoveTime;
    [SerializeField] private Vector2 targetPosition;

    private GameObject boss;
    private Vector2 currentVelocity;

    public override void EnterState(BossStateMachine stateMachine) {
        Debug.Log("Stationary");
        boss = stateMachine.gameObject;
    }

    public override void UpdateState(BossStateMachine stateMachine) {
        if (Vector2.Distance(boss.transform.position, targetPosition) > 0.01) {
            Vector2 newPosition = Vector2.SmoothDamp(boss.transform.position, targetPosition, ref currentVelocity,
                bossMoveTime);
            boss.transform.position = newPosition;
        }
    }
}
