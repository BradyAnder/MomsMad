using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] float currentTime;
    [SerializeField] float startingTime;

    [SerializeField] TextMeshProUGUI countDownText;
    

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        countDownText.color = Color.white;
        countDownText.fontSize = 50;
    }

    // Update is called once per frame
    void Update()
    {
        CountDown();
        ChangeColorText();
    }

    void CountDown()
    {
        if (currentTime > 0)
        {
            currentTime -= 1 * Time.deltaTime;
            int seconds = Mathf.FloorToInt(currentTime % 60);
            countDownText.text = seconds.ToString();
        }
        else
        {
            countDownText.text = "0";
        }
    }

    void ChangeColorText()
    {
        if(currentTime < startingTime / 2)
        {
            countDownText.color = Color.yellow;
            countDownText.fontSize = 60;
        }
        if(currentTime < 10)
        {
            countDownText.color = Color.red;
            countDownText.fontSize = 80;
        }
    }
}
