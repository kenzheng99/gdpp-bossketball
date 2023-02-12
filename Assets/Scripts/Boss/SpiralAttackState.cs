using UnityEngine;

[CreateAssetMenu(menuName = "BossStates/SpiralAttackState")]
public class SpiralAttackState: BossState {
    
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float durationSeconds;

    private Timer timer;
    public override void EnterState(BossStateMachine stateMachine) {
        Debug.Log("SpiralAttackState");
        timer = new Timer(durationSeconds);
    }

    public override void UpdateState(BossStateMachine stateMachine) {
        timer.Tick(Time.deltaTime);
        if (timer.Done()) {
            stateMachine.SwitchState(stateMachine.idleState);
        }
    }
}
