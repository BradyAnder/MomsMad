using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownManager : MonoBehaviour
{
    public Text countdownText; // Reference to the countdown text
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
            //print to console for testing
            Debug.Log(countdown);
            // We wait for 1 second
            yield return new WaitForSeconds(1);
            // We decrement the countdown value
            countdown--;
        }
        // We set the countdown text to "GO!"
        countdownText.text = "GO!";
        // We wait for 1 second
        yield return new WaitForSeconds(1f);
        // We set the countdown text to an empty string
        countdownText.text = "";
    }
}