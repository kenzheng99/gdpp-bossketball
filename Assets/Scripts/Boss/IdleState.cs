using UnityEngine;

[CreateAssetMenu(menuName = "BossStates/IdleState")]
public class IdleState : BossState {

    [SerializeField] private float durationSeconds;
    private Timer timer;
    public override void EnterState(BossStateMachine stateMachine) {
        Debug.Log("IdleState");
        timer = new Timer(durationSeconds);
    }

    public override void UpdateState(BossStateMachine stateMachine) {
        timer.Tick(Time.deltaTime);
        if (timer.Done()) {
            stateMachine.SwitchState(stateMachine.spiralAttackState);
        }
    }
}
