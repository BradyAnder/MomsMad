using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataSingleton : MonoBehaviour
{
    public static PlayerDataSingleton playerDataInstance { get; private set; }


    //Just a list of values to reference for each player
    List<string> playerNumbers = new List<string>
    {
        "Player 1",
        "Player 2",
        "Player 3",
        "Player 4",
        "Player 5"
    };

    //List of values to store which controller each player is using
    List<int> playerControllers = new List<int>();

    // Awake is called with spawned
    void Awake()
    {

        //If there is an instance, and it's not this instance, delete myself
        if (playerDataInstance != null && playerDataInstance != this)
        {
            Destroy(this);
        }
        else
        {
            playerDataInstance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Saves the index values of each a player controller to that player
    public static void SetPlayerController(string playerNumber, int controllerIndex)
    {
        for (int i =0; i < playerDataInstance.playerNumbers.Count; i++)
        {
            if(playerDataInstance.playerNumbers[i] == playerNumber)
            {
                playerDataInstance.playerControllers[i] = controllerIndex;
            }     
        }   
    }

    public static int GetPlayerController(string playerNumber)
    {
        for (int i = 0; i < playerDataInstance.playerNumbers.Count; i++)
        {
            if (playerDataInstance.playerNumbers[i] == playerNumber)
            {
                return playerDataInstance.playerControllers[i];
            }
        }

        return -1;
    }
}
