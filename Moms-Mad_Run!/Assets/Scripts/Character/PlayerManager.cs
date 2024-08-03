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

    public GameObject MomSpawnPoint;

    private List<PlayerInput> playerInputs = new List<PlayerInput>();
    private int currentRound = 0;

    private List<LobbyManager.Player> playerInfo = new List<LobbyManager.Player>();
    private ScoreRecorder scoreRecorder;

    void Start()
    {
        inGameScoreboard = FindObjectOfType<InGameScoreboard>();
        if (inGameScoreboard == null) { Debug.Log("PlayerManager: in-game scoreboard component not found."); }

        DetectPlayers();
        int len = playerInfo.Count;
        string[] playerNames = new string[len];
        for (int i = 0; i < len; i++)
        {
            playerNames[i] = "Player" +  playerInfo[i].playerNumber.ToString();
        }
        inGameScoreboard.playerNames = playerNames;

        InitializeScores();
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
        Dictionary<string, Color> playerColors = new Dictionary<string, Color>();
        foreach (LobbyManager.Player player in playerInfo)
        {
            scoreRecorder.AddScore(player, 0); // Initialize each player's score to 0
            if (ColorUtility.TryParseHtmlString(player.colour, out Color color))
            {
                playerColors.Add("Player" + player.playerNumber, color); // Add player color
            }
            else
            {
                Debug.LogWarning($"Invalid color string for Player {player.playerNumber}: {player.colour}");
            }
        }
        Debug.Log("Player colors initialized: " + playerColors.Count);
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
        if (RoundManager.round - 1 >= playerInfo.Count)
        {
            Debug.Log("All rounds completed!");
            inGameScoreboard.resetScore();
            return;
        }

        availableSpawnPoints = new List<GameObject>(childSpawnPoints);
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
        GameObject newObj = Instantiate(playerPrefab, MomSpawnPoint.transform.position, Quaternion.identity);
        GameObject momObj = newObj.GetComponentInChildren<MoveMom>().gameObject;
        GameObject childObj = newObj.GetComponentInChildren<MoveSlideChild>().gameObject;

        childObj.SetActive(false);

        PlayerInput currentPlayer = momObj.GetComponent<PlayerInput>();
        currentPlayer.SwitchCurrentControlScheme("controller", player.device);
        player.currentObj = momObj;
        AddPlayerNumberIndicator(momObj, player.playerNumber);
    }

    void SpawnChild(LobbyManager.Player player, List<GameObject> spawnPoints, int colourIndex)
    {
        int randomIndex = Random.Range(0, spawnPoints.Count);
        GameObject spawnPoint = spawnPoints[randomIndex];
        spawnPoints.RemoveAt(randomIndex);

        GameObject newObj = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
        GameObject momObj = newObj.GetComponentInChildren<MoveMom>().gameObject;
        GameObject childObj = newObj.GetComponentInChildren<MoveSlideChild>().gameObject;

        momObj.SetActive(false);

        childObj.GetComponent<MeshRenderer>().material = childColourMats[colourIndex];
        player.colour = "#" + ColorUtility.ToHtmlStringRGB(childColourMats[colourIndex].color);

        PlayerInput currentPlayer = childObj.GetComponent<PlayerInput>();
        currentPlayer.SwitchCurrentControlScheme("controller", player.device);
        player.currentObj = childObj;
        AddPlayerNumberIndicator(childObj, player.playerNumber);
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartRound();
        }

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

    public Dictionary<string, Color> GetPlayerColors()
    {
        Dictionary<string, Color> playerColors = new Dictionary<string, Color>();
        foreach (LobbyManager.Player player in playerInfo)
        {
            if (ColorUtility.TryParseHtmlString(player.colour, out Color color))
            {
                playerColors.Add("Player" + player.playerNumber, color);
            }
        }
        Debug.Log("Retrieved player colors: " + playerColors.Count);
        return playerColors;
    }
}
