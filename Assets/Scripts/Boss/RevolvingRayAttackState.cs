using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "BossStates/RevolvingRayAttackState")]
public class RevolvingRayAttackState: BossState {
    
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float stateTimerSeconds;
    [SerializeField] private float bossMoveTime;
    [SerializeField] private int numRays;
    [SerializeField] private int projectilesPerRay;
    [SerializeField] private float spacing;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float spawnTimeSeconds;
    [SerializeField] private Vector2 targetPosition;

    private GameObject boss;
    private bool[,] spawned;
    private List<GameObject> projectiles;
    private Timer stateTimer;
    private Timer spawnTimer;
    
    private bool doneSpawning;
    private Vector3 fireDir;
    private Vector2 currentVelocity;
    

    public override void EnterState(BossStateMachine stateMachine) {
        Debug.Log("RevolvingRayAttackState");
        SoundManager.Instance.PlayRevolvingAttackIntroSound();

        stateTimer = new Timer(stateTimerSeconds);
        
        boss = stateMachine.gameObject;
        
        fireDir = Vector3.up.normalized;
        spawned = new bool[numRays,projectilesPerRay];
        projectiles = new List<GameObject>();
        doneSpawning = false;
        spawnTimer = new Timer(spawnTimeSeconds);
    }

    public override void UpdateState(BossStateMachine stateMachine) {

        if (spawnTimer != null) {
            spawnTimer.Tick(Time.deltaTime);
            if (spawnTimer.Done()) {
                Physics2D.IgnoreLayerCollision(6, 9, false);
                spawnTimer = null;
            }
        }
        if (Vector2.Distance(boss.transform.position, targetPosition) < 0.01) {
            // arrived, spawn projectiles once
            if (doneSpawning) {
                // rotate them
                foreach (var p in projectiles) {
                    if (p != null) {
                        p.transform.RotateAround(targetPosition,
                            Vector3.back, rotationSpeed * Time.deltaTime);
                    }
                }
            }
            else {
                // spawn projectiles, give player invincibility while projectiles spawn
                Physics2D.IgnoreLayerCollision(6, 9, true);
                spawnTimer ??= new Timer(spawnTimeSeconds);
                doneSpawning = true;
                for (var i = 0; i < numRays; i++) {
                    for (var ii = 0; ii < projectilesPerRay; ii++) {
                        if (spawned[i, ii] == false) {
                            doneSpawning = false;
                            Vector3 projPos = boss.transform.position + (fireDir * ii * spacing);
                            projectiles.Add(Instantiate(projectilePrefab, projPos, Quaternion.identity));
                            spawned[i, ii] = true;
                        }
                    }
                    fireDir = Quaternion.Euler(0, 0, (-360 / (float)numRays)) * fireDir;
                }
            }
        }
        else {
            // move to target position
            Vector2 newPosition = Vector2.SmoothDamp(boss.transform.position, targetPosition, ref currentVelocity,
                bossMoveTime);
            boss.transform.position = newPosition;
        }
        
        stateTimer.Tick(Time.deltaTime);
        if (stateTimer.Done()) {
            foreach (var p in projectiles) {
                Destroy(p);
            }
            stateMachine.SwitchToRandomState();
        }
    }
}
