using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "BossStates/SpiralAttackState")]
public class SpiralAttackState: BossState {
    
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float postAttackWait;
    [SerializeField] private float bossMoveSpeed;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private int numProjectiles;
    [SerializeField] private int numAttacks;
    [SerializeField] private float coolDown;

    private Rigidbody2D bossRb;
    private Vector2 targetPosition;
    private Vector3 fireDir;
    private Timer fireCoolDown;
    private Timer timer;
    private int numFired;

    private Transform playerTr;
    private List<GameObject> projectiles;

    public override void EnterState(BossStateMachine stateMachine) {
        Debug.Log("SpiralAttackState");
        timer = new Timer(postAttackWait);
        fireCoolDown = new Timer(coolDown);
        
        bossRb = stateMachine.GetComponent<Rigidbody2D>();
        targetPosition = new Vector2(0, 6);
        
        numFired = 0;
        fireDir = Vector3.down;

        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        projectiles = new List<GameObject>();
    }

    public override void UpdateState(BossStateMachine stateMachine) {

        if (Vector2.Distance(bossRb.position, targetPosition) < 1) {
            // arrived, start attack
            if (fireCoolDown.Done() && numFired < numAttacks*numProjectiles) {
                projectiles.Add(FireProjectile(fireDir));
                fireDir = Quaternion.Euler(0, 0, (-360 / (float)numProjectiles)) * fireDir;
                fireCoolDown = new Timer(coolDown);
                numFired++;
            }
            fireCoolDown.Tick(Time.deltaTime);
        }
        else {
            Vector2 newPosition = Vector2.MoveTowards(bossRb.position, 
                targetPosition, bossMoveSpeed * Time.deltaTime);
            bossRb.MovePosition(newPosition);
        }

        foreach (var p in projectiles) {
            if (p != null) {
                p.GetComponent<Projectile>().SetTrajectory(playerTr.position - 
                    p.transform.position, projectileSpeed);
            }
        }

        if (numFired >= numAttacks * numProjectiles) {
            timer.Tick(Time.deltaTime);
        }
        if (timer.Done()) {
            stateMachine.SwitchState(stateMachine.idleState);
        }
    }

    private GameObject FireProjectile(Vector3 target) {
        Vector3 spawnPos = bossRb.position; // makes a V2 out of V2
        GameObject projectileObj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        Projectile projectile = projectileObj.GetComponent<Projectile>();
        projectile.SetTrajectory(target, projectileSpeed);
        return projectileObj; // return projectile lets you edit projectile trajectory later (player seeking)
    }
}
