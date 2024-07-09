using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    public static int round = 1;

    public static TextMeshProUGUI roundText;

    //Hold the current level scene
    Scene currentLevel;
    private static string currentLevelName = "Null";

    private void Awake()
    {
        // Initialize roundText in Awake
        roundText = GameObject.Find("RoundInfo").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        roundText.text = "Round " + round;
        Debug.Log("Start: Round Manager started. Round: " + round);

        //Get the current scene name
        currentLevel = SceneManager.GetActiveScene();
        currentLevelName = currentLevel.name;
        Debug.Log("Loaded Level: " + currentLevel.name);
    }

    public static void HandleRound()
    {
        int numPlayer = PlayerInputManager.instance.playerCount;
        Debug.Log("Handling Round. Current Round: " + round + ", NumPlayer: " + numPlayer);

        if (round < numPlayer)
        {
            round++;
            Debug.Log("Loading next round. New Round: " + round);

            //Reload the same level between rounds
            SceneManager.LoadScene(currentLevelName);
            Time.timeScale = 1;
        }
        else
        {
            Debug.Log("All rounds completed. Loading MainMenu.");
            SceneManager.LoadScene("Scoreboard");
            Time.timeScale = 1;
            LobbyManager.Instance.ResetLobby();
            ResetRound();
        }
    }

    public static void ResetRound()
    {
        round = 1;
    }
}
