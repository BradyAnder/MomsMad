using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    public TextMeshProUGUI roundText;
    public static RoundManager Instance;

    // Hold the current level scene
    private static string currentLevelName = "Null";

    private ScoreRecorder scoreRecorder;

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
        InitializeRoundText();
    }

    void Start()
    {
        InitializeRoundText();

        // Get the current scene name
        Scene currentLevel = SceneManager.GetActiveScene();
        if (currentLevel.name != "Leaderboard")
        {
            currentLevelName = currentLevel.name;
        }
        Debug.Log("Loaded Level: " + currentLevel.name);

        // Find ScoreRecorder instance
        scoreRecorder = FindObjectOfType<ScoreRecorder>();

        // Update the round text
        UpdateRoundText();
    }

    void Update()
    {
        // Get the current scene name
        Scene currentLevel = SceneManager.GetActiveScene();
        if (currentLevel.name != "Leaderboard")
        {
            currentLevelName = currentLevel.name;
        }

        // Reset Rounds on Main
        if (currentLevel.name == "MainMenu")
        {
            scoreRecorder?.ResetAll();
            Destroy(gameObject);
            Debug.Log("Destroyed RoundManager");
        }

        UpdateRoundText();
    }

    private void InitializeRoundText()
    {
        roundText = GameObject.Find("RoundInfo")?.GetComponent<TextMeshProUGUI>();
    }

    private void UpdateRoundText()
    {
        if (roundText != null && scoreRecorder != null)
        {
            roundText.text = "Round " + scoreRecorder.currRound;
            Debug.Log("Round text updated: " + roundText.text);
        }
        else
        {
            Debug.LogError("RoundInfo TextMeshProUGUI or ScoreRecorder not found.");
        }
    }

    public static void HandleRound()
    {
        RoundManager instance = FindObjectOfType<RoundManager>();
        ScoreRecorder scoreRecorder = FindObjectOfType<ScoreRecorder>();

        if (instance != null && scoreRecorder != null)
        {
            int numPlayer = PlayerInputManager.instance.playerCount;
            int round = scoreRecorder.currRound;
            Debug.Log("Handling Round. Current Round: " + round + ", NumPlayer: " + numPlayer);

            if (round < numPlayer)
            {
                // scoreRecorder.currRound++; // This will increment the round
                Debug.Log("Loading leaderboard. Round: " + scoreRecorder.currRound);
                SceneManager.LoadScene("Leaderboard");
            }
            else
            {
                Debug.Log("All rounds completed. Loading Scoreboard.");
                SceneManager.LoadScene("Scoreboard");
                Time.timeScale = 1;
                LobbyManager.Instance.ResetLobby();
                scoreRecorder.ResetAll();
            }
        }
        else
        {
            Debug.LogError("RoundManager or ScoreRecorder not found.");
        }
    }

    public int getRound()
    {
        return scoreRecorder != null ? scoreRecorder.currRound : 0;
    }

    public static void ReturnToGame()
    {
        RoundManager instance = FindObjectOfType<RoundManager>();
        ScoreRecorder scoreRecorder = FindObjectOfType<ScoreRecorder>();

        if (instance != null && scoreRecorder != null)
        {
            Debug.Log("Returning to game. Current Round: " + scoreRecorder.currRound);
            Debug.Log("Current level name: " + currentLevelName);
            if (!string.IsNullOrEmpty(currentLevelName))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(currentLevelName);
                SceneManager.sceneLoaded += OnSceneLoaded;
                Debug.Log("Scene loaded: " + currentLevelName);
            }
            else
            {
                Debug.LogError("Current level name is not set.");
            }
        }
        else
        {
            Debug.LogError("RoundManager or ScoreRecorder not found.");
        }
    }

    private static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        RoundManager instance = FindObjectOfType<RoundManager>();
        if (instance != null)
        {
            instance.InitializeRoundText();
            instance.UpdateRoundText();
        }
    }
}
