using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public GameObject childPrefab;
    public GameObject momPrefab;

    // List of child spawn points
    private List<Vector3> ChildSpawnPoints = new List<Vector3> 
    { 
        new Vector3(-18.34f, 1.73f, -13.33f), 
        new Vector3(-24.45f, 1.73f, -14.57f), 
        new Vector3(-21.81f, 2.09f, -7.43f), 
        new Vector3(-24.83f, 1.73f, -2.1f), 
        new Vector3(-24.83f, 1.73f, -5.74f), 
        new Vector3(-24.83f, 1.73f, -9.58f), 
        new Vector3(-18.99f, 1.73f, -2.16f), 
        new Vector3(-14.81f, 1.73f, 2.16f), 
        new Vector3(-18.69f, 1.73f, -6.79f) 
    };

    // The mom position is set at x = -5.82, y =1.91, z = -12.97 with rotation 0,0,0
    private Vector3 MomSpawnPoint = new Vector3(-5.82f, 1.91f, -12.97f);

    private List<PlayerInput> playerInputs = new List<PlayerInput>();
    private List<int> momOrder = new List<int>();
    private int currentRound = 0;

    void Start()
    {
        // Initialize the players
        DetectPlayers();
        // Set up the player roles for each round
        DetermineMomOrder();
        // Spawn the players for the first round
        StartRound();
    }

    private void DetectPlayers()
    {
        // Ensure there are exactly 5 players
        for (int i = 0; i < 5; i++)
        {
            if (i < Gamepad.all.Count)
            {
                var playerInput = PlayerInput.Instantiate(childPrefab, controlScheme: "Gamepad", pairWithDevice: Gamepad.all[i]);
                playerInputs.Add(playerInput);
                // we print how many players are connected
                Debug.Log("Player " + i + " connected");
            }
            else
            {
                Debug.LogError("Not enough gamepads connected!");
                return;
            }
        }
    }

    private void DetermineMomOrder()
    {
        // Create a list of player indices and shuffle it to determine the mom order
        List<int> playerIndices = new List<int>();
        for (int i = 0; i < playerInputs.Count; i++)
        {
            playerIndices.Add(i);
        }

        while (playerIndices.Count > 0)
        {
            int randomIndex = Random.Range(0, playerIndices.Count);
            momOrder.Add(playerIndices[randomIndex]);
            playerIndices.RemoveAt(randomIndex);
        }
    }

    void StartRound()
    {
        // Check for end of game
        if (currentRound >= playerInputs.Count)
        {
            // End of game
            Debug.Log("All rounds completed!");
            return;
        }

        // Get the mom player index for this round
        int momPlayerIndex = momOrder[currentRound];

        // Create a list of available child spawn points
        List<Vector3> availableChildSpawnPoints = new List<Vector3>(ChildSpawnPoints);

        for (int i = 0; i < playerInputs.Count; i++)
        {
            if (i == momPlayerIndex)
            {
                SpawnMom(playerInputs[i]);
            }
            else
            {
                SpawnChild(playerInputs[i], availableChildSpawnPoints);
            }
        }
        currentRound++;
    }

    void SpawnMom(PlayerInput player)
    {
        momPrefab.transform.position = MomSpawnPoint;
        momPrefab.transform.rotation = new Quaternion(0, 0, 0, 0);
        // player.SwitchCurrentControlScheme("Mom");
        // player.isMom = true;
        player.gameObject.name = "Mom";
    }

    void SpawnChild(PlayerInput player, List<Vector3> availableSpawnPoints)
    {
        // Choose a random spawn point for the child
        int randomIndex = Random.Range(0, 9);
        Vector3 spawnPoint = availableSpawnPoints[randomIndex];
        
       
        player.transform.position = spawnPoint;
        player.transform.rotation = new Quaternion(0, 0, 0, 0);
        // player.SwitchCurrentControlScheme("Child");
        // player.isMom = false;
        player.gameObject.name = "Child";

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