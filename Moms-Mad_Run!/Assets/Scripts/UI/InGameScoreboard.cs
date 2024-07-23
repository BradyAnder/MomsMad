using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InGameScoreboard : MonoBehaviour
{
    public string[] playerNames;
    public TextMeshProUGUI[] scoreboardRows;
    private InGameScoreboard instance;

    private static string currentLevelName = "Null"; // Hold the current level scene

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        SceneNameUpdate();
        if (currentLevelName == "MainMenu") //Destroys self if on main menu
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        Debug.Log("InGameScoreboard Debug");
        foreach (string i in playerNames) {
            Debug.Log(i);
        }
        if (playerNames == null) { Debug.Log("InGameScoreboard: player names not defined."); return; }
        if (scoreboardRows == null) { Debug.Log("InGameScoreboard: text component not specified."); return; }
        for (int i = playerNames.Length - 1; i < scoreboardRows.Length; i++) {
            scoreboardRows[i].text = "";
        }
    }

    public void updateScore(string playerName, int score) {
        int index = System.Array.IndexOf(playerNames, playerName);
        scoreboardRows[index].text = playerName + ": " + score.ToString();
    }

    public void resetScore()
    {
        foreach (TextMeshProUGUI text in scoreboardRows) {
            text.text = "";
        }
    }

    void SceneNameUpdate() //Update the scene name
    {
        // Get the current scene name
        Scene currentLevel = SceneManager.GetActiveScene();
        currentLevelName = currentLevel.name;
        //Debug.Log("Loaded Level: " + currentLevel.name);
    }
}
