using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

[CreateAssetMenu(menuName = "BossStates/HomingAttackState")]
public class HomingAttackState: BossState {
    
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float postAttackWait;
    [SerializeField] private float bossMoveTime;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private int numProjectiles;
    [SerializeField] private int numAttacks;
    [SerializeField] private float coolDown;
    [SerializeField] private Vector2 targetPosition;
    [SerializeField] private float randOffSet;
    [SerializeField] private float trajectoryDelay;

    private GameObject boss;
    private Vector3 fireDir;
    private Timer fireCoolDown;
    private Timer postAttackWaitTimer;
    private Timer trajectoryTimer;
    private int numFired;
    private Vector2 currentVelocity;
    
    private Transform playerTr;
    private List<GameObject> projectiles;

    public override void EnterState(BossStateMachine stateMachine) {
        Debug.Log("HomingAttackState");
        postAttackWaitTimer = new Timer(postAttackWait);
        fireCoolDown = new Timer(coolDown);
        
        boss = stateMachine.gameObject;
        
        numFired = 0;
        fireDir = Vector3.up;
        
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        projectiles = new List<GameObject>();
        trajectoryTimer = new Timer(trajectoryDelay);
    }

    public override void UpdateState(BossStateMachine stateMachine) {

        if (Vector2.Distance(boss.transform.position, targetPosition) < 0.5) {
            // arrived, start attack
            if (fireCoolDown.Done() && numFired < numAttacks*numProjectiles) {
                projectiles.Add(FireProjectile(fireDir));
                // fireDir = Quaternion.Euler(0, 0, (-360 / (float)numProjectiles)) * fireDir;
                fireCoolDown = new Timer(coolDown);
                numFired++;
            }
            fireCoolDown.Tick(Time.deltaTime);
        }
        else {
            // move to spot
            Vector2 newPosition = Vector2.SmoothDamp(boss.transform.position, targetPosition, ref currentVelocity,
                bossMoveTime);
            boss.transform.position = newPosition;
        }
        
        foreach (var p in projectiles) {
            if (p != null) {
                if (trajectoryTimer.Done()) {
                    Vector3 offSet = new Vector3(Random.Range(-randOffSet, randOffSet), 
                        Random.Range(-randOffSet, randOffSet), 0);
                    p.GetComponent<Projectile>().SetTrajectory(playerTr.position -
                        p.transform.position + offSet,
                        projectileSpeed);
                }
            }
        }

        if (trajectoryTimer.Done()) {
            trajectoryTimer = new Timer(trajectoryDelay);
        }
        else {
            trajectoryTimer.Tick(Time.deltaTime);        
        }
        
        if (numFired >= numAttacks * numProjectiles) {
            postAttackWaitTimer.Tick(Time.deltaTime);
        }
        if (postAttackWaitTimer.Done()) {
            // stateMachine.SwitchState(stateMachine.idleState);
        }
    }

    private GameObject FireProjectile(Vector3 target) {
        Vector3 spawnPos = boss.transform.position; // makes a V2 out of V2
        GameObject projectileObj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        // Projectile projectile = projectileObj.GetComponent<Projectile>();
        projectileObj.GetComponent<Projectile>().SetTrajectory(target, projectileSpeed);
        return projectileObj; // return projectile lets you edit projectile trajectory later (player seeking)
    }
}
