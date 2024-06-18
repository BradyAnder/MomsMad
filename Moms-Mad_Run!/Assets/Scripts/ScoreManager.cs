using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour 
{

    public static ScoreManager Instance { get; private set; }

    private int score = 0;

    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        scoreText.text = score.ToString();
    }

    public void AddScore(int add)
    {
        score += add;
    }

    public int GetScore()
    {
        return score;
    }
    public void ResetScore()
    {
        score = 0;
    }

}

