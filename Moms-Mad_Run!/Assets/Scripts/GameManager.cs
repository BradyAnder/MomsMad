using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = ScoreManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Lobby");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (scoreManager != null) {
            scoreManager.ResetScore();
        }

    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
