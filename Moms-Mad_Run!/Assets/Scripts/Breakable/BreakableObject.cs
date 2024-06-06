using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject brokenObject;
    private MoveSlideChild moveScript;
    public ScoreManager scoreManager;

    public int scoreValue;

    private void Start()
    {
        GameObject childObject = GameObject.Find("Child");
        GameObject scoreManagerObject = GameObject.Find("ScoreManager");
        scoreManager = scoreManagerObject.GetComponent<ScoreManager>();
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
                BreakObject();
            }
        }
    }
}
