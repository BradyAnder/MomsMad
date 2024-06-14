using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static LobbyManager;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public List<GameObject> childSpawnPoints = new List<GameObject>();
    List<GameObject> availableSpawnPoints = new List<GameObject>();

    public Material[] childColourMats;

    // The mom spawnPoint
    public GameObject MomSpawnPoint;

    private List<PlayerInput> playerInputs = new List<PlayerInput>();
    private List<int> momOrder = new List<int>();
    private int currentRound = 0;

    private List<Player> playerInfo = new List<Player>();

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
        playerInfo = LobbyManager.Instance.GetPlayers();

    }

    private void DetermineMomOrder()
    {
        // Create a list of player indices and shuffle it to determine the mom order
        List<int> playerIndices = new List<int>();
        for (int i = 0; i < playerInfo.Count; i++)
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
        if (currentRound >= playerInfo.Count)
        {
            // End of game
            Debug.Log("All rounds completed!");
            return;
        }

        // Get the mom player index for this round
        int momPlayerIndex = momOrder[currentRound];

        //Reset the available spawn points to the entire list
        availableSpawnPoints =childSpawnPoints;

        //Crude index to track number of child players spawned for changing colours
        int colourIndex = 0;

        for (int i = 0; i < playerInfo.Count; i++)
        {
            if (i == momPlayerIndex)
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
        colourIndex = 0;

        currentRound++;
    }

    void SpawnMom(Player player)
    {
        //Instantiate a new player and recognize it's Mom and Child objects
        GameObject newObj = Instantiate(playerPrefab, MomSpawnPoint.transform.position, Quaternion.identity);
        GameObject momObj = newObj.GetComponentInChildren<MoveMom>().gameObject;
        GameObject childObj = newObj.GetComponentInChildren<MoveSlideChild>().gameObject;

        //Turn off the child object
        childObj.SetActive(false);

        //Set the correct device to this player
        PlayerInput currentPlayer = momObj.GetComponent<PlayerInput>();
        currentPlayer.SwitchCurrentControlScheme("controller", player.device);
 
    }

    void SpawnChild(Player player, List<GameObject> spawnPoints, int ColourValue)
    {
        // Choose a random spawn point for the child
        int randomIndex = Random.Range(0, spawnPoints.Count);
        GameObject spawnPoint = spawnPoints[randomIndex];
        availableSpawnPoints.Remove(spawnPoint);

        //Instantiate a new player and recognize it's Mom and Child objects
        GameObject newObj = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
        GameObject momObj = newObj.GetComponentInChildren<MoveMom>().gameObject;
        GameObject childObj = newObj.GetComponentInChildren<MoveSlideChild>().gameObject;

        //Turn the mom object off
        momObj.SetActive(false);

        //Change the childs colour based on which child it is
        childObj.GetComponent<MeshRenderer>().material = childColourMats[ColourValue];

        //Set the correct device to this player
        PlayerInput currentPlayer = childObj.GetComponent<PlayerInput>();
        currentPlayer.SwitchCurrentControlScheme("controller", player.device);
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