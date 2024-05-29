using UnityEngine;

public class ScoreManager : MonoBehaviour 
{

    public static ScoreManager Instance { get; private set; }

    private int score = 0;


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


    public void AddScore(int add)
    {
        score += add;
    }

    public int GetScore()
    {
        return score;
    }

}

