using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LeaderBoardManager : MonoBehaviour
{
    public GameObject title;
    public GameObject[] scoreboardRows;
    public string[] defaultChars = { "Mom", "Child1", "Child2", "Child3", "Child4", "Child5" };
    public int[] defaultScores = { 0, 0, 0, 0, 0, 0 };

    private int round_num;
    private bool useDefault;
    private TextMeshProUGUI tempScoreText;
    private int totalChildScore = 0;
    private ScoreRecorder scoreRecorder;
    private List<PlayerData> playerDataList;

    private void Awake()
    {
        // Initialization that does not rely on other scripts or external resources.
    }

    private void Start()
    {
        if (RoundManager.Instance == null)
        {
            Debug.LogError("RoundManager instance not found.");
            return;
        }

        // round_num = RoundManager.Instance.getRound() - 1;

        scoreRecorder = FindObjectOfType<ScoreRecorder>();
        round_num = scoreRecorder.currRound;
        Debug.Log("Round number: " + round_num);
        if (scoreRecorder == null)
        {
            Debug.LogError("Score Recorder not found.");
            return;
        }

        string[] playerObjects = scoreRecorder.PlayerObjectsToArray();
        int[] playerScores = scoreRecorder.PlayerScoresToArray();
        Color[] playerColors = scoreRecorder.PlayerColorsToArray();

        if (playerObjects == null || playerScores == null)
        {
            Debug.LogError("Player objects or scores not found in ScoreRecorder.");
            return;
        }

        Debug.Log("Player objects length: " + playerObjects.Length);
        Debug.Log("Player scores length: " + playerScores.Length);

        int playerNumber = playerObjects.Length;

        if (playerScores.Length < playerNumber)
        {
            Debug.LogError("Player scores are not initialized properly.");
            useDefault = true;
            playerDataList = new List<PlayerData>();
            for (int i = 0; i < defaultChars.Length; i++)
            {
                playerDataList.Add(new PlayerData { Name = defaultChars[i], Score = defaultScores[i], PlayerColor = new Color(0.5f, 0.5f, 0.5f) });
            }
        }
        else
        {
            playerDataList = new List<PlayerData>();
            for (int i = 0; i < playerObjects.Length; i++)
            {
                playerDataList.Add(new PlayerData { Name = playerObjects[i], Score = playerScores[i], PlayerColor = playerColors[i] });
            }

            playerDataList.Sort((x, y) => y.Score.CompareTo(x.Score));
        }

        if (title == null)
        {
            Debug.LogError("UI title not assigned");
            return;
        }

        if (scoreboardRows == null || scoreboardRows.Length < playerNumber)
        {
            Debug.LogError("UI text rows not assigned or insufficient rows.");
            return;
        }

        int highestScore = playerDataList[0].Score;

        for (short i = 0; i < playerNumber; i++)
        {
            if (scoreboardRows[i] == null)
            {
                Debug.LogError($"Scoreboard row {i} is not assigned.");
                continue;
            }

            tempScoreText = scoreboardRows[i].GetComponent<TextMeshProUGUI>();
            if (tempScoreText == null)
            {
                Debug.LogError("Unexpected object with no TMP component.");
                continue;
            }

            int tempScore = playerDataList[i].Score;
            Color color = playerDataList[i].PlayerColor;
            if (i > 0) { totalChildScore += tempScore; }

            string displayText = playerDataList[i].Name + ": " + tempScore;
            if (tempScore == highestScore)
            {
                displayText = displayText + "👑"; // Adding a crown emoji for the highest score
            }

            tempScoreText.text = displayText;
            tempScoreText.color = color;
        }

        tempScoreText = title.GetComponent<TextMeshProUGUI>();
        if (tempScoreText == null)
        {
            Debug.LogError("Unexpected object with no TMP component.");
            return;
        }

        tempScoreText.text = "Leaderboard for Round: " + round_num;

        // Wait for 5 seconds and then return to the game
        // Debug.Log("Starting coroutine to wait for 5 seconds");
        StartCoroutine(ReturnToGameAfterDelay(3));
    }

    private IEnumerator ReturnToGameAfterDelay(float delay)
    {
        Debug.Log("Waiting for " + delay + " seconds before returning to game.");
        float previousTimeScale = Time.timeScale;
        Time.timeScale = 1f; // Ensure time scale is 1 during wait

        Debug.Log("Current time scale set to: " + Time.timeScale);
        yield return new WaitForSeconds(delay);

        Debug.Log("Finished waiting. Returning to game.");
        Time.timeScale = previousTimeScale; // Restore previous time scale
        Debug.Log("Time scale restored to: " + Time.timeScale);
        scoreRecorder.currRound++;
        SceneManager.LoadScene(scoreRecorder.levelSelected);
        // RoundManager.ReturnToGame();
    }

    private class PlayerData
    {
        public string Name { get; set; }
        public int Score { get; set; }

        public Color PlayerColor { get; set; }
    }
}
