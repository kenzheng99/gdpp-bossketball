using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {
    PLAYING, 
    WIN, 
    GAMEOVER
};
public class GameManager : MonoBehaviour
{
    private static GameManager _instance; // set up the singleton
    private GameState currentState = GameState.PLAYING;
    
    // Objects
    [SerializeField] private Boss boss;
    [SerializeField] private Boss bossPrefab;
    [SerializeField] private PlayerHealthController player;
    
    // UI elements
    [SerializeField] private BossHealthBar bossHealthBar;
    [SerializeField] private PlayerHealthUI playerHealthUI;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject gameOverScreen;

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
        if (health <= 0) {
            Win();
        }
    }

    public void UpdatePlayerHealth(int health) {
        playerHealthUI.SetHealth(health);
        if (health <= 0) {
            GameOver();
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
        
        boss = Instantiate(bossPrefab, bossStartPosition, Quaternion.identity);
        player.ResetPlayer();
    }

    public void QuitGame() {
        Application.Quit();
    }

    private void GameOver() {
        currentState = GameState.GAMEOVER;
        gameOverScreen.SetActive(true);
    }

    private void Win() {
        currentState = GameState.WIN;
        winScreen.SetActive(true);
    }
}
