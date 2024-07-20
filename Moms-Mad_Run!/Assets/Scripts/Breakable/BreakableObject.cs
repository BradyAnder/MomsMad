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

    public Material outline;
    public Material[] materials;
    public bool isBreakable = false;


    public int scoreValue;

    private void Start()
    {

        Renderer renderer = GetComponent<Renderer>();
        if(renderer != null)
        {
            materials = renderer.materials;
            outline = materials.Length > 1 ? materials[1] : null;
        }
        GameObject childObject = GameObject.Find("Child");
        GameObject scoreManagerObject = GameObject.Find("ScoreManager");
        scoreManager = scoreManagerObject.GetComponent<ScoreManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        if (playerManager == null) { Debug.Log("BreakableScoring->Start: playerManager not found."); return; }

        ToggleOutline(false);
    }

    private void Update()
    {
        breakable();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            moveScript = collision.gameObject.GetComponent<MoveSlideChild>();
            if (collision.gameObject.tag == "Player" && moveScript.isSliding == true && isBreakable == true)
            {
                Instantiate(brokenObject, transform.position, transform.rotation);
                playerManager.AddScore(collision.gameObject, scoreValue);
                Destroy(gameObject);

               if(ScoreTextPopUp)
                {
                    ShowScoreText();
                }
            }
        }
    }

    void ShowScoreText()
    {
       GameObject scorePopUP = Instantiate(ScoreTextPopUp, transform.position, Quaternion.identity);
       TMP_Text scoreText = scorePopUP.GetComponentInChildren<TMP_Text>();
       scoreText.text = scoreValue.ToString();
    }

    void breakable()
    {
        if (isBreakable == true)
        {
            ToggleOutline(true);
        }
        else
        {
            ToggleOutline(false);
        }
    }

    public void ToggleOutline (bool enable)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null && outline != null)
        {
            if (enable)
            {
                materials[1] = outline; // Enable outline
            }
            else
            {
                materials[1] = null; // Disable outline
            }
            renderer.materials = materials;
        }
    }
}
