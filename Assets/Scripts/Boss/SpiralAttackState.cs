using UnityEngine;

[CreateAssetMenu(menuName = "BossStates/SpiralAttackState")]
public class SpiralAttackState: BossState {
    
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float postAttackWait;
    [SerializeField] private float bossMoveTime;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private int numProjectiles;
    [SerializeField] private int numAttacks;
    [SerializeField] private float coolDown;
    [SerializeField] private Vector2 targetPosition;

    private GameObject boss;
    private Vector3 fireDir;
    private Timer fireCoolDown;
    private Timer postAttackWaitTimer;
    private int numFired;
    private Vector2 currentVelocity;

    public override void EnterState(BossStateMachine stateMachine) {
        Debug.Log("SpiralAttackState");
        postAttackWaitTimer = new Timer(postAttackWait);
        fireCoolDown = new Timer(coolDown);
        
        boss = stateMachine.gameObject;
        
        numFired = 0;
        fireDir = Vector3.down;
    }

    public override void UpdateState(BossStateMachine stateMachine) {

        if (Vector2.Distance(boss.transform.position, targetPosition) < 0.01) {
            // arrived, start attack
            // TODO fix bug with fire cooldown not working properly
            if (fireCoolDown.Done() && numFired < numAttacks*numProjectiles) {
                FireProjectile(fireDir);
                fireDir = Quaternion.Euler(0, 0, (-360 / (float)numProjectiles)) * fireDir;
                fireCoolDown = new Timer(coolDown);
                numFired++;
            }
            fireCoolDown.Tick(Time.deltaTime);
        }
        else {
            // Vector2 newPosition = Vector2.MoveTowards(boss.transform.position, 
            //     targetPosition, bossMoveSpeed * Time.deltaTime);
            Vector2 newPosition = Vector2.SmoothDamp(boss.transform.position, targetPosition, ref currentVelocity,
                bossMoveTime);
            boss.transform.position = newPosition;
        }

        if (numFired >= numAttacks * numProjectiles) {
            postAttackWaitTimer.Tick(Time.deltaTime);
        }
        if (postAttackWaitTimer.Done()) {
            stateMachine.SwitchState(stateMachine.idleState);
        }
    }

    private Projectile FireProjectile(Vector3 target) {
        Vector3 spawnPos = boss.transform.position; // makes a V2 out of V2
        GameObject projectileObj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        Projectile projectile = projectileObj.GetComponent<Projectile>();
        projectile.SetTrajectory(target, projectileSpeed);
        return projectile; // return projectile lets you edit projectile trajectory later (player seeking)
    }
}
