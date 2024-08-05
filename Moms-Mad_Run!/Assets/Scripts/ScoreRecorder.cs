using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreRecorder : MonoBehaviour
{
    private ArrayList playerObjects;
    private ArrayList playerScores;
    private static ScoreRecorder instance;
    private int playerNumber = 0;
    public bool isDebugMode = false;
    public InGameScoreboard inGameScoreboard;

    public int maxRound;
    public int currRound;
    public string levelSelected;

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
        maxRound = 0;
        currRound = 0;
        levelSelected = "";

        if (isDebugMode)
        {
            LobbyManager.Player p1 = new LobbyManager.Player();
            LobbyManager.Player p2 = new LobbyManager.Player();
            LobbyManager.Player p3 = new LobbyManager.Player();
            AddScore(p1, 100);
            AddScore(p2, 200);
            AddScore(p3, 300);
            AddScore(p2, 200);
            UnityEngine.SceneManagement.SceneManager.LoadScene("Scoreboard");
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
            index = playerObjects.IndexOf(playerObj);
        }
        else {
            playerScores[index] = (int)playerScores[index] + amount;
        }
        if (inGameScoreboard == null) { return; }
        inGameScoreboard.updateScore(playerObj.playerNumber, (int)playerScores[index]);
    }

    public string[] PlayerObjectsToArray() {
        string[] arr = new string[playerNumber];
        for (int i = 0; i < playerNumber; i++) {
            arr[i] = "Player " + ((LobbyManager.Player)playerObjects[i]).playerNumber.ToString();
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

    public Color[] PlayerColorsToArray() {
        Color[] arr = new Color[playerNumber];
        for (int i = 0; i < playerNumber; i++) {
            arr[i] = ((LobbyManager.Player)playerObjects[i]).colour;
        }
        return arr;
    }

    public void ResetAll() {
        playerObjects.Clear();
        playerScores.Clear();
        playerNumber = 0;
        maxRound = 0;
        currRound = 0;
        levelSelected = "";
        inGameScoreboard.resetScore();
    }
}
