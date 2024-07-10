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
    public static RoundManager Instance;

    // Hold the current level scene
    private static string currentLevelName = "Null";

    private void Awake()
    {
        // Ensure singleton instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize roundText in Awake
        roundText = GameObject.Find("RoundInfo")?.GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        if (roundText != null)
        {
            roundText.text = "Round " + round;
            Debug.Log("Start: Round Manager started. Round: " + round);
        }

        // Get the current scene name
        Scene currentLevel = SceneManager.GetActiveScene();
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
            Debug.Log("Loading leaderboard. Round: " + round);
            SceneManager.LoadScene("Leaderboard");
        }
        else
        {
            Debug.Log("All rounds completed. Loading Scoreboard.");
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

    public int getRound()
    {
        return round;
    }

    public static void ReturnToGame()
    {
        Debug.Log("Returning to game. Current Round: " + round);
        Debug.Log("Current level name: " + currentLevelName);
        if (!string.IsNullOrEmpty(currentLevelName))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(currentLevelName);
            Debug.Log("Scene loaded: " + currentLevelName);
        }
        else
        {
            Debug.LogError("Current level name is not set.");
        }
    }
}
