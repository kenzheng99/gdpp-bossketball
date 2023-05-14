using UnityEngine;

public class Boss : MonoBehaviour
{
    public int maxHealth;
    [SerializeField] private ParticleSystem bossDeathParticles;
    private Rigidbody2D rb;
    [SerializeField] private BossStateMachine _bossStateMachine;
    public int currentHealth;
    [SerializeField] private GameObject bossModel;
    public bool hasEnteredPhaseTwo = false;
    [SerializeField] private BossEyeball eyeball;
    private GameManager gameManager;
    private Vector3 startingPosition;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
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
        if (currentHealth <= 0)
        {
            BossDeath();
        }
        // enters phase 2 at 50% health
        if (currentHealth <= (0.5 * maxHealth) && hasEnteredPhaseTwo == false)
        {
            GameManager.Instance.bossPhaseTwo = true;
            hasEnteredPhaseTwo = true;
            eyeball.SetRed();
            SoundManager.Instance.PlayBossPhaseTwoIntroSound();
        }
    }

    public void ResetBoss() {
        currentHealth = maxHealth;
        gameManager.UpdateBossHealth(currentHealth);
        transform.position = startingPosition;
        gameManager.bossPhaseTwo = false;
        eyeball.SetBlue();
        BossStateMachine stateMachine = GetComponent<BossStateMachine>();
        stateMachine.SwitchState(stateMachine.initialState);
    }

    public int GetMaxHealth() {
        return maxHealth;
    }

    public void BossDeath()
    {
        SoundManager.Instance.PlayBossDeathSound();
        //turn off colliders 
        GetComponent<CircleCollider2D>().enabled = false;
        // ignore collision of player and boss projectiles
        Physics2D.IgnoreLayerCollision(6, 9, true);
        //turn off boss state machine
        _bossStateMachine.enabled = false;
        //play particle effect
        var particleEmission = bossDeathParticles.emission;
        var particleDuration = bossDeathParticles.duration;
        particleEmission.enabled = true;
        bossDeathParticles.Play();
        // delay win until boss death finishes
        Invoke(nameof(MakeBossDisappear), particleDuration+1f);
        Invoke(nameof(Win), particleDuration+1.5f);
        
    }
    public void MakeBossDisappear()
    {
        bossModel.transform.localScale = Vector3.zero;
    }

    public void Win()
    {
        GameManager.Instance.Win();
    }
}
