using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreRecorder : MonoBehaviour
{
    private ArrayList playerObjects;
    private ArrayList playerScores;
    private static ScoreRecorder instance;
    private int playerNumber = 0;
    public bool isDebugMode = false;

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

        playerObjects = new ArrayList();
        playerScores = new ArrayList();

        if (isDebugMode)
        {

        }
        //////////////////////
    }

    public void AddPlayer(GameObject playerObj) {
        playerObjects.Add(playerObj);
        playerScores.Add(0);
    }

    /// <summary>
    /// This is the only function call for adding/deducting scores in game scene.
    /// Scripts handling scoring events in the game scene should invoke this function so that the score can be reflected in the scoreboard scene.
    /// Importantly, the scoreboard components, including this one, does not know how many players there are. Only if a score is ever added for a player, would these components learn about the existence of that player. Therefore, it may be ideal to call this function to add 0 score for every players at the beginning so that they are sure to be displayed in the scoreboard.
    /// </summary>
    /// <param name="playerObj"> The Player object reference of the player to add score to. </param>
    /// <param name="amount"> The amount of score to add (negative for deduction). </param>
    public void AddScore(LobbyManager.Player playerObj, int amount) {
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

    public string[] PlayerObjectsToArray() {
        string[] arr = new string[playerNumber];
        for (int i = 0; i < playerNumber; i++) {
            arr[i] = ((LobbyManager.Player)playerObjects[i]).name;
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
