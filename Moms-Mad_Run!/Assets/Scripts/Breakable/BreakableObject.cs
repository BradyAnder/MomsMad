using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject brokenObject;
    public GameObject ScoreTextPopUp;
    private MoveSlideChild moveScript;
    public ScoreManager scoreManager;
    private PlayerManager playerManager;


    public int scoreValue;

    private void Start()
    {
        GameObject childObject = GameObject.Find("Child");
        GameObject scoreManagerObject = GameObject.Find("ScoreManager");
        scoreManager = scoreManagerObject.GetComponent<ScoreManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        if (playerManager == null) { Debug.Log("BreakableScoring->Start: playerManager not found."); return; }
    }


    void BreakObject()
    {
        Instantiate(brokenObject, transform.position, transform.rotation);
        scoreManager.AddScore(scoreValue);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            moveScript = collision.gameObject.GetComponent<MoveSlideChild>();
            if (collision.gameObject.tag == "Player" && moveScript.isSliding == true)
            {
                Instantiate(brokenObject, transform.position, transform.rotation);
                playerManager.AddScore(collision.gameObject, scoreValue);
                Destroy(gameObject);
               if(ScoreTextPopUp)
                {
                    ShowScoreText();
                }
                BreakObject();
            }
        }
    }

    void ShowScoreText()
    {
       GameObject scorePopUP = Instantiate(ScoreTextPopUp, transform.position, Quaternion.identity);
       TMP_Text scoreText = scorePopUP.GetComponentInChildren<TMP_Text>();
       scoreText.text = scoreValue.ToString();
    }
}
