using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossStateMachine : MonoBehaviour {

    [SerializeField] public BossState initialState;
    [SerializeField] public IdleState idleState;
    [SerializeField] public SpiralAttackState spiralAttackState;
    [SerializeField] public StationaryState stationaryState;
    [SerializeField] public HomingAttackState homingAttackState;
    [SerializeField] public DashAttackState dashAttackState;


    private BossState[] stateList;
    private BossState currentState;
    void Start() {
        stateList = new BossState[] {idleState, spiralAttackState, homingAttackState, dashAttackState};
        currentState = initialState;
        initialState.EnterState(this);
    }
    void Update() {
        if (GameManager.Instance.CurrentState == GameState.PLAYER_DEAD) {
            return;
        }
        currentState.UpdateState(this);
    }

    public void SwitchState(BossState newState) {
        currentState = newState;
        currentState.EnterState(this);
    }

    public void SwitchToRandomState() {
        int randomIndex = Random.Range(0, stateList.Length);
        while (stateList[randomIndex] == currentState) {
            randomIndex = Random.Range(0, stateList.Length);
        }
        SwitchState(stateList[randomIndex]);
}
    
}
