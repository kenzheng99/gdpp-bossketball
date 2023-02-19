using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour {

    public void StartGame() {
        SceneManager.LoadScene("SampleScene");
        SoundManager.Instance.StartMusic();
    }
    public void Quit() {
        Application.Quit();
    }
}
