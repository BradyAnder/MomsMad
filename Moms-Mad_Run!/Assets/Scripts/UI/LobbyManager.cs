using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance { get; private set; }

    private List<Player> players = new List<Player>();
    private bool allReady = false;

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
            Player newPlayer = new Player { device = gamepad, isReady = false };
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
    bool loadScene = true;
    void Update()
    {
        foreach (Player player in players)
        {
            if (player.device.buttonSouth.wasPressedThisFrame)
            {
                player.isReady = !player.isReady;
                Debug.Log("Gamepad " + (players.IndexOf(player) + 1) + " is " + (player.isReady ? "ready" : "not ready") + ".");
            }
        }

        allReady = players.Count > 0 && players.TrueForAll(p => p.isReady);
        //Check if all controllers ready
        if (allReady)
        {
            Debug.Log("All players are ready. Starting the game...");
            if (loadScene == true)
            {
                SceneManager.LoadScene("FirstLevel");
                loadScene = false;
            }
        }
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
    }
}
