using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Count : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rsCountDown;
    [SerializeField] float three;
    [SerializeField] float two;
    [SerializeField] float one;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDown());
        rsCountDown.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator CountDown()
    {
        rsCountDown.text = three.ToString();
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1);
        rsCountDown.text = two.ToString();
        yield return new WaitForSecondsRealtime(1); 
        rsCountDown.text = one.ToString();
        yield return new WaitForSecondsRealtime(1);
        rsCountDown.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
