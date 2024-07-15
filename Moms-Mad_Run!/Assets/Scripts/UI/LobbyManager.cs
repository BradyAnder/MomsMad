using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance { get; private set; }

    public GameObject playerPrefab;

    private List<Player> players = new List<Player>();
    private List<GameObject> playerObjs = new List<GameObject>();
    private bool allReady = false;
    private bool loadScene = false;
    private string[] defaultPlayerNames = {"P1", "P2", "P3", "P4", "P5", "P6"};
    private short i = 0;
    private ScoreRecorder scoreRecorder;

    // Hold the current level scene
    private static string currentLevelName = "Null";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        SceneNameUpdate();

        foreach (var gamepad in Gamepad.all)
        {
            AddPlayer(gamepad);
        }
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    void SceneNameUpdate() //Update the scene name
    {
        // Get the current scene name
        Scene currentLevel = SceneManager.GetActiveScene();
        currentLevelName = currentLevel.name;
        //Debug.Log("Loaded Level: " + currentLevel.name);
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (device is Gamepad gamepad)
        {
            switch (change)
            {
                case InputDeviceChange.Added:
                    AddPlayer(gamepad);
                    break;
                case InputDeviceChange.Removed:
                    RemovePlayer(gamepad);
                    break;
            }
        }
    }

    private void AddPlayer(Gamepad gamepad)
    {
        if (!players.Exists(p => p.device == gamepad))
        {
            Player newPlayer = new Player { device = gamepad, isReady = false, name = defaultPlayerNames[i] };
            i++;
            players.Add(newPlayer);
            Debug.Log("Gamepad " + (players.Count) + " connected.");
        }
    }

    private void RemovePlayer(Gamepad gamepad)
    {
        Player player = players.Find(p => p.device == gamepad);
        if (player != null)
        {
            players.Remove(player);
            Debug.Log("Gamepad " + (players.Count + 1) + " disconnected.");
        }
    }

    void Update()
    {
        if (currentLevelName != "Lobby2")
        {
            SceneNameUpdate();
        }
        if (currentLevelName == "Lobby2") //Avoid ready up on main menu
        {
            foreach (Player player in players)
            {
                if (player.device.buttonWest.wasPressedThisFrame)
                {
                    player.isReady = !player.isReady;
                }
            }
        }

        allReady = players.Count > 0 && players.TrueForAll(p => p.isReady);
        if (allReady)
        {
            if (loadScene)
            {
                scoreRecorder = FindObjectOfType<ScoreRecorder>();
                scoreRecorder.ResetAll();
                SceneManager.LoadScene("Level Select");
                loadScene = false;
                SceneNameUpdate();
            }
        }
    }

    public void ResetLobby()
    {
        loadScene = true;
        foreach (Player player in players)
        {
            player.isReady = false;
        }
    }
    public void SetLoadSceneFalse(){
        loadScene = false;
    }

    public List<Player> GetPlayers()
    {
        return players;
    }

    public bool AreAllPlayersReady()
    {
        return allReady;
    }

    public class Player
    {
        public Gamepad device;
        public bool isReady;
        public string colour;
        public string name;
        public GameObject currentObj;
        public int playerNumber;
    }
}
