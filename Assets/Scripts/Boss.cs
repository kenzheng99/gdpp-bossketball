using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private GameManager gameManager;
    private Vector3 startingPosition;

    void Start() {
        gameManager = GameManager.Instance;
        
        currentHealth = maxHealth;
        gameManager.UpdateBossHealth(currentHealth);
        startingPosition = transform.position;
    }

    void Update()
    {
        //TODO delete this, placeholder to test health bar
        if (Input.GetKeyDown(KeyCode.Tab)) {
            BossTakeDamage(10);
        }
    }

    public void BossTakeDamage(int damage)
    {
        currentHealth -= damage;
        gameManager.UpdateBossHealth(currentHealth);
    }

    public void ResetBoss() {
        currentHealth = maxHealth;
        gameManager.UpdateBossHealth(currentHealth);
        transform.position = startingPosition;
        BossStateMachine stateMachine = GetComponent<BossStateMachine>();
        stateMachine.SwitchState(stateMachine.initialState);
    }

    public int GetMaxHealth() {
        return maxHealth;
    }
}
