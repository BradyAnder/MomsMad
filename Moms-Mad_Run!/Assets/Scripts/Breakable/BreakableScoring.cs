using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The new breakable object script that uses ScoreRecorder.
/// </summary>
public class BreakableScoring : MonoBehaviour
{
    public GameObject brokenObject;
    private MoveSlideChild moveScript;
    private ScoreRecorder scoreRecorder;

    public int scoreValue;

    private void Start()
    {
        GameObject childObject = GameObject.Find("Child");
        scoreRecorder = FindObjectOfType<ScoreRecorder>();
        if (scoreRecorder == null) { Debug.Log("BreakableScoring->Start: scoreRecorder not found."); return; }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            moveScript = collision.gameObject.GetComponent<MoveSlideChild>();
            if (collision.gameObject.tag == "Player" && moveScript.isSliding == true)
            {
                Instantiate(brokenObject, transform.position, transform.rotation);
                scoreRecorder.AddScore(collision.gameObject, scoreValue);
                Destroy(gameObject);
            }
        }
    }
}
