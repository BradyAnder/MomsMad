using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public List<GameObject> childSpawnPoints = new List<GameObject>();
    List<GameObject> availableSpawnPoints = new List<GameObject>();
    private InGameScoreboard inGameScoreboard;
    public Material[] childColourMats;
    public GameObject playerNumberIndicator;
    public Color[] playerColors = { new Color(1, 0, 0), new Color(1, 1, 0), new Color(0.5f, 0, 0.5f), new Color(1, 0.5f, 0) };

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
            playerNames[i] = "Player" +  playerInfo[i].playerNumber.ToString();
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
        for (int i = 0; i < playerInfo.Count; i++) {
            playerInfo[i].playerNumber = i + 1;
        }
    }

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

    void AddPlayerNumberIndicator(GameObject playerObj, int playerNumber, Color color)
    {
        GameObject numIndicator = Instantiate(playerNumberIndicator, playerObj.transform);
        numIndicator.transform.localPosition = Vector3.up * 2;
        TextMeshPro temp = numIndicator.GetComponent<TextMeshPro>();
        temp.text = "Player " + playerNumber.ToString();
        temp.color = color;
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
        if (scoreRecorder.currRound - 1 >= playerInfo.Count)
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
        int layer = 8;
        for (int i = 0; i < playerInfo.Count; i++)
        {
            if (i == 0) { layer = 8; }
            if (i == 1) { layer = 9; }
            if (i == 2) { layer = 11; }
            if (i == 3) { layer = 10; }
            if (i == scoreRecorder.currRound - 1)
            {
                Debug.Log("Spawned Mom");
                SpawnMom(playerInfo[i], colourIndex, layer);
                colourIndex++;
            }
            else
            {
                Debug.Log("Spawned Child");
                SpawnChild(playerInfo[i], availableSpawnPoints, colourIndex, layer);
                colourIndex++;
            }
            layer++;
        }

        currentRound++;
    }

    void SpawnMom(LobbyManager.Player player, int colourIndex, int layer)
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
        momObj.GetComponent<MeshRenderer>().material = childColourMats[colourIndex];
        momObj.layer = layer;
        player.currentObj = momObj;
        AddPlayerNumberIndicator(momObj, player.playerNumber, playerColors[colourIndex]);
    }

    void SpawnChild(LobbyManager.Player player, List<GameObject> spawnPoints, int colourIndex, int layer)
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
        childObj.layer = layer;
        AddPlayerNumberIndicator(childObj, player.playerNumber, playerColors[colourIndex]);
    }

    void Update()
    {
        // For testing: press the space key to start the next round
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            // We spawn a new child 
            // StartRound();
        }

        // Ensure all player number indicators face the camera
        FaceIndicatorsToCamera();
    }

    void FaceIndicatorsToCamera()
    {
        Camera mainCamera = Camera.main;
        TextMeshPro[] indicators = FindObjectsOfType<TextMeshPro>();
        foreach (TextMeshPro indicator in indicators)
        {
            indicator.transform.LookAt(indicator.transform.position + mainCamera.transform.rotation * Vector3.forward,
                                       mainCamera.transform.rotation * Vector3.up);
        }
    }
}
