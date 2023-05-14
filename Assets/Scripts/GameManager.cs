using System.Collections;
using UnityEngine;

public enum GameState {
    BEFORE_BATTLE,
    PLAYING, 
    PLAYER_DEAD,
    BOSS_DEAD,
    WIN, 
    GAME_OVER
};
public class GameManager : MonoBehaviour
{
    private static GameManager _instance; // set up the singleton

    public GameState CurrentState { get; private set; }
    
    // Objects
    [SerializeField] private Boss boss;
    [SerializeField] private Boss bossPrefab;
    [SerializeField] private PlayerHealthController player;
    
    // UI elements
    [SerializeField] private BossHealthBar bossHealthBar;
    [SerializeField] private PlayerHealthUI playerHealthUI;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject gameOverScreen;

    public bool bossPhaseTwo = false;

    private Vector3 bossStartPosition;

    public static GameManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }
    
    void Start() {
        winScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        bossStartPosition = boss.transform.position;
        bossHealthBar.SetMaxHealth(boss.GetMaxHealth());
        bossHealthBar.SetHealth(boss.GetMaxHealth());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            QuitGame();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            ResetGame();
        }
    }

    public void UpdateBossHealth(int health) {
        bossHealthBar.SetHealth(health);
    }

    public void UpdatePlayerHealth(int health) {
        playerHealthUI.SetHealth(health);
        if (health <= 0) {
            CurrentState = GameState.PLAYER_DEAD;
            Debug.Log("player dead");
            StartCoroutine(GameOver());
        }
    }
    
    
    public void ResetGame() {
        gameOverScreen.SetActive(false);
        winScreen.SetActive(false);
        Destroy(boss.gameObject);
        GameObject[] bossProjectiles = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject projectile in bossProjectiles) {
            Destroy(projectile);
        }
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }

        bossPhaseTwo = false;
        boss = Instantiate(bossPrefab, bossStartPosition, Quaternion.identity);
        player.ResetPlayer();
        Time.timeScale = 1;
    }

    public void QuitGame() {
        Application.Quit();
    }

    private IEnumerator GameOver() {
        yield return new WaitForSeconds(3);
        Debug.Log("game over");
        CurrentState = GameState.GAME_OVER;
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void Win() 
    {
        CurrentState = GameState.WIN;
        winScreen.SetActive(true);
        Time.timeScale = 0;
    }
}
