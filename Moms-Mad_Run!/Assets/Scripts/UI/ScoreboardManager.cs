using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreboardManager : MonoBehaviour
{
    public GameObject title;
    public GameObject[] scoreboardRows;
    public string[] defaultChars = { "Mom", "Child1", "Child2", "Child3", "Child4", "Child5" };
    public int[] defaultScores = { 0, 0, 0, 0, 0, 0 };
    public int playerNumber = 6;
    public int momSlightlyMadScore = 0;
    public int momModeratelyMadScore = 500;
    public int momVeryMadScore = 1000;
    public string momSlightlyMadText = "Mom is slightly mad";
    public string momModeratelyMadText = "Mom is moderately mad";
    public string momVeryMadText = "Mom is very mad";

    private bool useDefault;
    private TextMeshProUGUI tempScoreText;
    private int totalChildScore = 0;
    private ScoreRecorder scoreRecorder;
    private List<PlayerData> playerDataList;

    private void Awake()
    {
    }

    private void Start()
    {
        scoreRecorder = FindObjectOfType<ScoreRecorder>();
        if (scoreRecorder == null)
        {
            Debug.Log("Score Recorder not found.");
            return;
        }
        string[] playerObjects = scoreRecorder.PlayerObjectsToArray();
        int[] playerScores = scoreRecorder.PlayerScoresToArray();
        Color[] playerColors = scoreRecorder.PlayerColorsToArray();
        playerNumber = playerScores.Length;

        if (playerObjects == null || playerScores == null)
        {
            Debug.LogError("Player objects or scores not found in ScoreRecorder.");
            return;
        }

        if (playerScores.Length < playerNumber)
        {
            useDefault = true;
            playerDataList = new List<PlayerData>();
            for (int i = 0; i < defaultChars.Length; i++)
            {
                playerDataList.Add(new PlayerData { Name = defaultChars[i], Score = defaultScores[i], PlayerColor = new Color(0.8f, 0.8f, 0.8f)});
            }
        }
        else
        {
            playerDataList = new List<PlayerData>();
            for (int i = 0; i < playerObjects.Length; i++)
            {
                playerDataList.Add(new PlayerData { Name = playerObjects[i], Score = playerScores[i], PlayerColor = playerColors[i]});
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
            if (i > 0) { totalChildScore += tempScore; }

            string displayText = playerDataList[i].Name + ": " + tempScore;
            tempScoreText.text = displayText;
            tempScoreText.color = playerDataList[i].PlayerColor;
        }

        tempScoreText = title.GetComponent<TextMeshProUGUI>();
        if (tempScoreText == null)
        {
            Debug.LogError("Unexpected object with no TMP component.");
            return;
        }

        if (totalChildScore >= momSlightlyMadScore) { tempScoreText.text = momSlightlyMadText; }
        if (totalChildScore >= momModeratelyMadScore) { tempScoreText.text = momModeratelyMadText; }
        if (totalChildScore >= momVeryMadScore) { tempScoreText.text = momVeryMadText; }

        scoreRecorder.ResetAll(); // Reset the score recorder for next round.
        return;
    }

    private class PlayerData
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public Color PlayerColor { get; set; }
    }
}
