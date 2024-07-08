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

    //Random Play Level
    public int randomLevel;

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
        foreach (var gamepad in Gamepad.all)
        {
            AddPlayer(gamepad);
        }
        InputSystem.onDeviceChange += OnDeviceChange;
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
        foreach (Player player in players)
        {
            if (player.device.buttonSouth.wasPressedThisFrame)
            {
                player.isReady = !player.isReady;
            }
        }

        allReady = players.Count > 0 && players.TrueForAll(p => p.isReady);
        if (allReady)
        {
            if (loadScene)
            {
                scoreRecorder = FindObjectOfType<ScoreRecorder>();
                scoreRecorder.ResetAll();

                randomLevel = Random.Range(1, 4); //Random Level Generator
                Debug.Log(randomLevel);
                if (randomLevel == 1)
                {
                    SceneManager.LoadScene("Prototype 1");
                }
                else if (randomLevel == 2)
                {
                    SceneManager.LoadScene("Prototype 2");
                }
                else if (randomLevel == 3)
                {
                    SceneManager.LoadScene("Prototype 3");
                }
                else
                {
                    Debug.LogError("Randomizer Broke.");
                }

                loadScene = false;
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
    }
}
