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
    // TODO: change this to the actual number of players i.e. numPlayer = PlayerInputManager.instance.playerCount;
    public static int numPlayer = 3; 
    
    public static TextMeshProUGUI roundText;
    
    // Start is called before the first frame update
    void Start()
    {
        roundText = GameObject.Find("RoundInfo").GetComponent<TextMeshProUGUI>();
        roundText.text = "Round " + round;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void HandleRound()
    {
        if (round < numPlayer)
        {
            round++;
            SceneManager.LoadScene("MultipleRounds");
            // We need this. Otherwise, all the scripts are disabled
            Time.timeScale = 1;
            roundText.text = "Round " + round;
        }
        else
        {
            SceneManager.LoadScene("Lose");
            // We need this. Otherwise, all the scripts are disabled
            Time.timeScale = 1;
        }
    }
}
