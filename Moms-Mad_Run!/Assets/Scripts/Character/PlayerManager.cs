using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public List<GameObject> childSpawnPoints = new List<GameObject>();
    List<GameObject> availableSpawnPoints = new List<GameObject>();
    private InGameScoreboard inGameScoreboard;
    public Material[] childColourMats;

    // The mom spawnPoint
    public GameObject MomSpawnPoint;

    private List<PlayerInput> playerInputs = new List<PlayerInput>();
    private int currentRound = 0;

    private List<LobbyManager.Player> playerInfo = new List<LobbyManager.Player>();
    private ScoreRecorder scoreRecorder;

    void Start()
    {
        inGameScoreboard = FindObjectOfType<InGameScoreboard>();
        if (inGameScoreboard == null) { Debug.Log("PlayerManager: in-game scoreboard component not found."); }
        // Initialize the players
        DetectPlayers();
        int len = playerInfo.Count;
        string[] playerNames = new string[len];
        for (int i= 0; i < len; i++)
        {
            playerNames[i] = playerInfo[i].name;
        }
        inGameScoreboard.playerNames = playerNames;
        // Initialize the scores for each player
        InitializeScores();
        // Spawn the players for the first round
        StartRound();
    }

    private void DetectPlayers()
    {
        playerInfo = LobbyManager.Instance.GetPlayers();
    }

    // TODO
    private void InitializeScores()
    {
        scoreRecorder = FindObjectOfType<ScoreRecorder>();
        scoreRecorder.inGameScoreboard = this.inGameScoreboard;
        inGameScoreboard.enabled = true;
        foreach (LobbyManager.Player player in playerInfo)
        {
            scoreRecorder.AddScore(player, 0); // Initialize each player's score to 0
        }
    }

    public void AddScore(GameObject playerObj, int amount) {
        foreach (LobbyManager.Player player in playerInfo) {
            if (player.currentObj.Equals(playerObj)) {
                scoreRecorder.AddScore(player, amount);
            }
        }
    }

    void StartRound()
    {
        // Check for end of game
        if (RoundManager.round - 1 >= playerInfo.Count)
        {
            // End of game
            Debug.Log("All rounds completed!");
            inGameScoreboard.resetScore();
            return;
        }

        // Reset the available spawn points to the entire list
        availableSpawnPoints = new List<GameObject>(childSpawnPoints);

        // Crude index to track number of child players spawned for changing colours
        int colourIndex = 0;

        for (int i = 0; i < playerInfo.Count; i++)
        {
            if (i == RoundManager.round - 1)
            {
                Debug.Log("Spawned Mom");
                SpawnMom(playerInfo[i]);
            }
            else
            {
                Debug.Log("Spawned Child");
                SpawnChild(playerInfo[i], availableSpawnPoints, colourIndex);
                colourIndex++;
            }
        }

        currentRound++;
    }

    void SpawnMom(LobbyManager.Player player)
    {
        // Instantiate a new player and recognize it's Mom and Child objects
        GameObject newObj = Instantiate(playerPrefab, MomSpawnPoint.transform.position, Quaternion.identity);
        GameObject momObj = newObj.GetComponentInChildren<MoveMom>().gameObject;
        GameObject childObj = newObj.GetComponentInChildren<MoveSlideChild>().gameObject;

        // Turn off the child object
        childObj.SetActive(false);

        // Set the correct device to this player
        PlayerInput currentPlayer = momObj.GetComponent<PlayerInput>();
        currentPlayer.SwitchCurrentControlScheme("controller", player.device);
        player.currentObj = momObj;
    }

    void SpawnChild(LobbyManager.Player player, List<GameObject> spawnPoints, int colourIndex)
    {
        // Choose a random spawn point for the child
        int randomIndex = Random.Range(0, spawnPoints.Count);
        GameObject spawnPoint = spawnPoints[randomIndex];
        spawnPoints.RemoveAt(randomIndex); // Remove the chosen spawn point from the list

        // Instantiate a new player and recognize it's Mom and Child objects
        GameObject newObj = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
        GameObject momObj = newObj.GetComponentInChildren<MoveMom>().gameObject;
        GameObject childObj = newObj.GetComponentInChildren<MoveSlideChild>().gameObject;

        // Turn the mom object off
        momObj.SetActive(false);

        // Change the child's colour based on which child it is
        childObj.GetComponent<MeshRenderer>().material = childColourMats[colourIndex];

        // Set the correct device to this player
        PlayerInput currentPlayer = childObj.GetComponent<PlayerInput>();
        currentPlayer.SwitchCurrentControlScheme("controller", player.device);
        player.currentObj = childObj;
    }

    void Update()
    {
        // For testing: press the space key to start the next round
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // We spawn a new child 
            StartRound();
        }
    }
}
