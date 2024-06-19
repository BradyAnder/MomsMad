using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownManager : MonoBehaviour
{
    public Text countdownText; // Reference to the countdown text
    public PlayerManager playerManager; // Reference to the PlayerManager script
    void Start()
    {
        // We start the countdown at the start of the round
        StartCoroutine(CountdownToStart());
    }

    public IEnumerator CountdownToStart()
    {
        // We start the countdown at 3
        int countdown = 3;
        while (countdown > 0)
        {
            // We set the countdown text to the current countdown value
            countdownText.text = countdown.ToString();
            // We wait for 1 second
            yield return new WaitForSeconds(1);
            // We decrement the countdown value
            countdown--;
        }
        // We set the countdown text to "GO!"
        countdownText.text = "GO!";
        // We wait for 1 second
        yield return new WaitForSeconds(1);
        // We set the countdown text to an empty string
        countdownText.text = "";
        // We start the round
        playerManager.StartRound();
    }
}