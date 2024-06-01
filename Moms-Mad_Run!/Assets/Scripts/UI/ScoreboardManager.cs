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
    public int[] actualScores;
    public short playerNumber = 6;
    public int momSlightlyMadScore = 0;
    public int momModeratelyMadScore = 500;
    public int momVeryMadScore = 1000;
    public string momSlightlyMadText = "Mom is slightly mad";
    public string momModeratelyMadText = "Mom is moderately mad";
    public string momVeryMadText = "Mom is very mad";

    private bool useDefaultScores;
    private TextMeshProUGUI tempScoreText;
    private int totalChildScore = 0;

    private void OnEnable()
    {
        if (title == null) {
            Debug.Log("UI title not assigned");
            return;
        }
        if (scoreboardRows == null) {
            Debug.Log("UI text rows not assigned.");
            return;
        }
        if (actualScores == null || actualScores.Length < playerNumber) {
            useDefaultScores = true;
        }
        int tempScore;
        for (short i = 0; i < playerNumber; i++) {
            tempScoreText = scoreboardRows[i].GetComponent<TextMeshProUGUI>();
            if (tempScoreText == null) {
                Debug.Log("Unexpected object with no TMP component.");
                return;
            }
            if (useDefaultScores) { tempScore = defaultScores[i]; }
            else { tempScore = actualScores[i]; }
            if (i > 0) { totalChildScore += tempScore; }
            tempScoreText.text = defaultChars[i] + ": " + tempScore;
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
        
        return;
    }

}
