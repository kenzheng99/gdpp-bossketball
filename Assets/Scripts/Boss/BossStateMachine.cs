using System;
using UnityEngine;

public class BossStateMachine : MonoBehaviour {

    [SerializeField] public BossState initialState;
    [SerializeField] public IdleState idleState;
    [SerializeField] public SpiralAttackState spiralAttackState;
    
    private BossState currentState;
    void Start() {
        currentState = initialState;
        initialState.EnterState(this);
    }
    void Update() {
        currentState.UpdateState(this);
    }

    public void SwitchState(BossState newState) {
        currentState = newState;
        currentState.EnterState(this);
    }

    void SwitchToRandomState() {
        throw new NotImplementedException();
    }
    
}
