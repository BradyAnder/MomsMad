using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    // round starts from 1
    public static int round = 1;
    
    public static TextMeshProUGUI roundText;

    private void Awake()
    {
        // Initialize roundText in Awake
        roundText = GameObject.Find("RoundInfo").GetComponent<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        roundText.text = "Round " + round;
        Debug.Log("Start: Round Manager started. Round: " + round);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void HandleRound()
    {
        int numPlayer = PlayerInputManager.instance.playerCount;
        Debug.Log("Handling Round. Current Round: " + round + ", NumPlayer: " + numPlayer);

        if (round < numPlayer)
        {
            round++;
            Debug.Log("Loading next round. New Round: " + round);
            SceneManager.LoadScene("Prototype 2");
            // We need this. Otherwise, all the scripts are disabled
            Time.timeScale = 1;
        }
        else
        {
            Debug.Log("All rounds completed. Loading MainMenu.");
            SceneManager.LoadScene("MainMenu");
            // We need this. Otherwise, all the scripts are disabled
            Time.timeScale = 1;
        }
    }
}
