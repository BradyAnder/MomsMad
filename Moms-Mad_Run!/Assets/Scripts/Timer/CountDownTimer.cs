using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CountDownTimer : MonoBehaviour
{
    [SerializeField] float currentTime;
    [SerializeField] float startingTime;

    [SerializeField] TextMeshProUGUI countDownText;
    [SerializeField] TextMeshProUGUI timesUpText;
    

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        countDownText.color = Color.white;
        countDownText.fontSize = 50;
        timesUpText.gameObject.SetActive(false);
        countDownText.gameObject.SetActive(true);
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
            timesUpText.gameObject.SetActive(true);
            countDownText.gameObject.SetActive(false);
            Time.timeScale = 0;
            StartCoroutine(ChangeScene());
        }
    }

    void ChangeColorText()
    {
        if(currentTime < startingTime / 2 + 1)
        {
            countDownText.color = Color.yellow;
            countDownText.fontSize = 65;
        }
        if(currentTime <= 11)
        {
            countDownText.color = Color.red;
            countDownText.fontSize = 85;
        }
    }

    IEnumerator ChangeScene()
    {
        yield return new WaitForSecondsRealtime(1f);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        
        RoundManager.HandleRound();
    }
}
