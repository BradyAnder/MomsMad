using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreboardManager : MonoBehaviour
{
    public GameObject title;
    public GameObject[] scoreboardRows;
    public string[] defaultChars = { "Mom", "Child1", "Child2", "Child3", "Child4", "Child5" };
    public int[] defaultScores = {0, 0, 0, 0, 0, 0};
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
    private string[] actualChars;
    private int[] actualScores;

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
        GameObject[] playerObjects = scoreRecorder.PlayerObjectsToArray();
        actualScores = scoreRecorder.PlayerScoresToArray();
        playerNumber = actualScores.Length;
        actualChars = new string[playerNumber];
        for (int i = 0; i < playerNumber; i++)
        {
            actualChars[i] = playerObjects[i].name;
        }

        if (title == null) {
            Debug.Log("UI title not assigned");
            return;
        }
        if (scoreboardRows == null) {
            Debug.Log("UI text rows not assigned.");
            return;
        }
        if (actualScores == null || actualScores.Length < playerNumber) {
            useDefault = true;
        }
        int tempScore;
        for (short i = 0; i < playerNumber; i++) {
            tempScoreText = scoreboardRows[i].GetComponent<TextMeshProUGUI>();
            if (tempScoreText == null) {
                Debug.Log("Unexpected object with no TMP component.");
                return;
            }
            if (useDefault) { tempScore = defaultScores[i]; }
            else { tempScore = actualScores[i]; }

            if (i > 0) { totalChildScore += tempScore; }

            if (useDefault) { tempScoreText.text = defaultChars[i] + ": " + tempScore; }
            else { tempScoreText.text = actualChars[i] + ": " + tempScore; }
        }
        tempScoreText = title.GetComponent<TextMeshProUGUI>();
        if (tempScoreText == null)
        {
            Debug.Log("Unexpected object with no TMP component.");
            return;
        }
        if (totalChildScore >= momSlightlyMadScore) { tempScoreText.text = momSlightlyMadText; }
        if (totalChildScore >= momModeratelyMadScore) { tempScoreText.text = momModeratelyMadText; }
        if (totalChildScore >= momVeryMadScore) { tempScoreText.text = momVeryMadText; }

        scoreRecorder.ResetAll(); // Reset the score recorder for next round.
        return;
    }

}
