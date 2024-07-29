using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayGame()
    {
        Time.timeScale = 1f;
        LobbyManager.Instance.ResetLobby();
        SceneManager.LoadScene("Lobby2");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        LobbyManager.Instance.SetLoadSceneFalse();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void GoToOptions()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Options");
        LobbyManager.Instance.SetLoadSceneFalse();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void GoToCredits()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Credits");
        LobbyManager.Instance.SetLoadSceneFalse();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
