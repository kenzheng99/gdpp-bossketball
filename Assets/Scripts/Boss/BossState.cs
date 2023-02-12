using UnityEngine;

public abstract class BossState : ScriptableObject {
    public abstract void EnterState(BossStateMachine stateMachine);
    
    public abstract void UpdateState(BossStateMachine stateMachine);
}
