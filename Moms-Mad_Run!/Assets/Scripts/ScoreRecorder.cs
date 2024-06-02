using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour
{
    private ArrayList playerObjects;
    private ArrayList playerScores;
    private static ScoreRecorder instance;
    private int playerNumber = 0;

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

    private void Start()
    {
        playerObjects = new ArrayList();
        playerScores = new ArrayList();
    }

    public void AddPlayer(GameObject playerObj) {
        playerObjects.Add(playerObj);
        playerScores.Add(0);
    }

    public void AddScore(GameObject playerObj, int amount) {
        int index = playerObjects.IndexOf(playerObj);
        if (index < 0)
        {
            playerObjects.Add(playerObj);
            playerScores.Add(amount);
            playerNumber++;
        }
        else {
            playerScores[index] = (int)playerScores[index] + amount;
        }
    }

    public GameObject[] PlayerObjectsToArray() {
        GameObject[] arr = new GameObject[playerNumber];
        for (int i = 0; i < playerNumber; i++) {
            arr[i] = (GameObject)playerObjects[i];
        }
        return arr;
    }

    public int[] PlayerScoresToArray() {
        int[] arr = new int[playerNumber];
        for (int i = 0; i < playerNumber; i++)
        {
            arr[i] = (int)playerScores[i];
        }
        return arr;
    }

    public void ResetAll() {
        playerObjects.Clear();
        playerScores.Clear();
        playerNumber = 0;
    }
}
