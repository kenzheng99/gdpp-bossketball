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
    [SerializeField] private PlayerHealthController player;
    
    // UI elements
    [SerializeField] private BossHealthBar bossHealthBar;
    [SerializeField] private PlayerHealthUI playerHealthUI;

    public static GameManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<GameManager>();
            }

            return _instance;
        }
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
    }
    void ResetGame() {
        boss.ResetBoss();
        player.ResetPlayer();
    }

    void QuitGame() {
        Application.Quit();
    }
}
