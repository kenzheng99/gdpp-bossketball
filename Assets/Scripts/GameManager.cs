using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager S; // set up the singleton
    public enum GameState { menu, getReady, playing, oops, gameOver };
    public GameState gameState = GameState.playing;

    private void Awake()
    {
        if (GameManager.S)
        {
            // game manager exists, destroy this object
            Destroy(this.gameObject);
        }
        else
        {
            S = this;
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
        else if (gameState == GameState.playing)
        {

        }
        
        else if (gameState == GameState.gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                StartANewGame();
            }
        }
    }

    void StartANewGame()
    {

    }
}
