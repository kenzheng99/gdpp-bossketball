using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "BossStates/RevolvingRayAttackState")]
public class RevolvingRayAttackState: BossState {
    
    [SerializeField] private GameObject projectilePrefab;
    // [SerializeField] private float postAttackWait;
    [SerializeField] private float bossMoveTime;
    // [SerializeField] private float projectileSpeed;
    [SerializeField] private int projectilesPerRay;
    [SerializeField] private int spacing;
    // [SerializeField] private float coolDown;
    [SerializeField] private Vector2 targetPosition;

    private GameObject boss;
    private List<GameObject> projectiles;
    private bool spawnOnce;
    private Vector3 fireDir;
    private Timer fireCoolDown;
    private Timer postAttackWaitTimer;
    private int numFired;
    private Vector2 currentVelocity;

    public override void EnterState(BossStateMachine stateMachine) {
        Debug.Log("RevolvingRayAttackState");
        SoundManager.Instance.PlaySpiralAttackIntroSound();
        if (GameManager.Instance.bossPhaseTwo == true)
        {
            SoundManager.Instance.PlayBossGenericRoarSound();
        }
        // postAttackWaitTimer = new Timer(postAttackWait);
        // fireCoolDown = new Timer(coolDown);
        
        boss = stateMachine.gameObject;
        
        numFired = 0;
        fireDir = Vector3.down.normalized;
        spawnOnce = false;
        projectiles = new List<GameObject>();
    }

    public override void UpdateState(BossStateMachine stateMachine) {

        if (Vector2.Distance(boss.transform.position, targetPosition) < 0.01) {
            // arrived, start attack
            if (!spawnOnce) {
                for (var i = 1; i <= projectilesPerRay; i++) {
                    Vector3 projPos = boss.transform.position + (fireDir * i * spacing);
                    projectiles.Add(Instantiate(projectilePrefab, projPos, Quaternion.identity));
                }

                spawnOnce = true;
            }

            // if (fireCoolDown.Done() && numFired < numAttacks*numProjectiles) {
            //     if (numFired % numProjectiles == 0) {
            //         fireDir = Quaternion.Euler(0, 0, Random.Range(0,50)) * fireDir;
            //     }
            //     FireProjectile(fireDir);
            //     fireDir = Quaternion.Euler(0, 0, (-360 / (float)numProjectiles)) * fireDir;
            //     fireCoolDown = new Timer(coolDown);
            //     numFired++;
            // }
            // fireCoolDown.Tick(Time.deltaTime);
        }
        else {
            // move to target position
            Vector2 newPosition = Vector2.SmoothDamp(boss.transform.position, targetPosition, ref currentVelocity,
                bossMoveTime);
            boss.transform.position = newPosition;
        }

        // if (numFired >= numAttacks * numProjectiles) {
        //     postAttackWaitTimer.Tick(Time.deltaTime);
        // }
        // if (postAttackWaitTimer.Done()) {
            // stateMachine.SwitchToRandomState();
        // }
    }

    private GameObject PlaceProjectile(Vector3 position) {
        // Vector3 spawnPos = boss.transform.position; // makes a V2 out of V2
        // GameObject projectileObj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        // Projectile projectile = projectileObj.GetComponent<Projectile>();
        // projectile.SetTrajectory(target, projectileSpeed);
        GameObject projectileObj = Instantiate(projectilePrefab, position, Quaternion.identity);
        return projectileObj; // return projectile lets you edit projectile trajectory later (player seeking)
    }
}
