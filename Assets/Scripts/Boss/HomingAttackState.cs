using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using Random = UnityEngine.Random;

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
    
    [SerializeField] private float waitTimeSeconds;
    [SerializeField] private Bounds moveBounds;
    [SerializeField] private float directionTimeSeconds;
    [SerializeField] private float moveDirectionDelta;
    [SerializeField] private float moveSpeed;

    private GameObject boss;
    private Vector3 fireDir;
    private Timer fireCoolDown;
    private Timer postAttackWaitTimer;
    private Timer trajectoryTimer;
    private int numFired;
    private Vector2 currentVelocity;
    
    private Timer directionTimer;
    private Vector3 moveDir;
    private float dirDelta;
    
    private Transform playerTr;
    private List<GameObject> projectiles;
    private bool arrivedAtStart;

    public override void EnterState(BossStateMachine stateMachine) {
        Debug.Log("HomingAttackState");
        if (GameManager.Instance.bossPhaseTwo == true)
        {
            SoundManager.Instance.PlayBossGenericRoarSound();
        }
        SoundManager.Instance.PlayHomingAttackIntroSound();
        postAttackWaitTimer = new Timer(postAttackWait);
        fireCoolDown = new Timer(coolDown);
        
        boss = stateMachine.gameObject;
        
        numFired = 0;
        fireDir = Vector3.up;
        
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        projectiles = new List<GameObject>();
        trajectoryTimer = new Timer(trajectoryDelay);
        
        directionTimer = new Timer(directionTimeSeconds);
        moveDir = Vector3.down;
        dirDelta = moveDirectionDelta;
    }

    public override void UpdateState(BossStateMachine stateMachine) {

        if (!arrivedAtStart && Vector2.Distance(boss.transform.position, targetPosition) < 0.5) {
            arrivedAtStart = true;
        }
        if (arrivedAtStart) {
            // arrived, start attack
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
            
            if (fireCoolDown.Done() && numFired < numAttacks*numProjectiles) {
                projectiles.Add(FireProjectile(fireDir));
                fireCoolDown = new Timer(coolDown);
                numFired++;
            }
            fireCoolDown.Tick(Time.deltaTime);
        }
        else {
            // move to starting position
            Vector2 newPosition = Vector2.SmoothDamp(boss.transform.position, targetPosition, ref currentVelocity,
                bossMoveTime);
            boss.transform.position = newPosition;
        }
        
        // make homing
        foreach (var p in projectiles) {
            if (p != null) {
                if (trajectoryTimer.Done()) {
                    Vector3 offSet = new Vector3(Random.Range(-randOffSet, randOffSet), 
                        Random.Range(-randOffSet, randOffSet), 0);
                    Vector3 target = playerTr.position -
                        p.transform.position + offSet;
                    if (p.transform.position.y < 0 && target.y < 0) {
                        target.y *= -1;
                    }
                    p.GetComponent<Projectile>().SetTrajectory(target,
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
            stateMachine.SwitchToRandomState();
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
