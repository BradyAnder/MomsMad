using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    //Random Play Level
    public int randomLevel;

    public void Lobby()
    {
        Time.timeScale = 1f;
        LobbyManager.Instance.ResetLobby();
        SceneManager.LoadScene("Lobby2");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void RandomLevel()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        randomLevel = Random.Range(1, 4); //Random Level Generator
        Debug.Log(randomLevel);
        if (randomLevel == 1)
        {
            SceneManager.LoadScene("Prototype 1");
        }
        else if (randomLevel == 2)
        {
            SceneManager.LoadScene("Prototype 2");
        }
        else if (randomLevel == 3)
        {
            SceneManager.LoadScene("Prototype 3");
        }
        else
        {
            Debug.LogError("Randomizer Broke.");
        }
    }

    public void Level1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Prototype 1");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void Level2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Prototype 2");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    public void Level3()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Prototype 3");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
}
