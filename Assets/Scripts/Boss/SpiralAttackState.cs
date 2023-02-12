using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "BossStates/SpiralAttackState")]
public class SpiralAttackState: BossState {
    
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float durationSeconds;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float projectileSpeed;
    
    private Rigidbody2D bossRb;
    private GameObject player;
    private Vector2 targetPosition;
    private Timer timer;
    private bool attacked;
    public override void EnterState(BossStateMachine stateMachine) {
        Debug.Log("SpiralAttackState");
        timer = new Timer(durationSeconds);
        
        bossRb = stateMachine.GetComponent<Rigidbody2D>();
        targetPosition = new Vector2(0, 6);

        player = GameObject.FindGameObjectWithTag("Player");
        attacked = false;
    }

    public override void UpdateState(BossStateMachine stateMachine) {

        if (Vector2.Distance(bossRb.position, targetPosition) < 2) {
            // arrived, start attack
            if (!attacked) {
                FireProjectile(Vector3.left);
                FireProjectile(Vector3.right);
                attacked = true;
            }
            Debug.Log("player pos"+ player.transform.position);
        }
        else {
            Vector2 newPosition = Vector2.MoveTowards(bossRb.position, 
                targetPosition, moveSpeed * Time.deltaTime);
            bossRb.MovePosition(newPosition);
        }
        
        // timer.Tick(Time.deltaTime);
        if (timer.Done()) {
            stateMachine.SwitchState(stateMachine.idleState);
        }
    }

    private Projectile FireProjectile(Vector3 target) {
        Vector3 spawnPos = bossRb.position;
        GameObject projectileObj = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
        Projectile projectile = projectileObj.GetComponent<Projectile>();
        projectile.SetTrajectory(target, projectileSpeed);
        return projectile;
    }
}
