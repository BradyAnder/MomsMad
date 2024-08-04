using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject brokenObject;
    [Space(1)]
    [Header("Score Text")]
    public GameObject ScoreTextPopUp;
    public GameObject spawnTextPoint;
    [Space(1)]

    private MoveSlideChild moveScript;
    public ScoreManager scoreManager;
    private PlayerManager playerManager;
    [Space(1)]
    [Header("Outliner")]
    public Material outline;
    public Material baseMat;
    public Material[] materials;
    public bool isBreakable = false;

    public int scoreValue;

    private void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        materials = renderer.materials;
        if (materials.Length > 1)
        {
            outline = materials[1];
            baseMat = materials[0]; 
        }
        else
        {
            outline = baseMat;
        }

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
       GameObject scorePopUP = Instantiate(ScoreTextPopUp, spawnTextPoint.transform.position, Quaternion.identity);
       TMP_Text scoreText = scorePopUP.GetComponentInChildren<TMP_Text>();
       scoreText.text = scoreValue.ToString();
    }

    void breakable()
    {
        if (isBreakable == true)
        {
            ToggleOutline(true);
            LayerBreakable(true);
        }
        else
        {
            ToggleOutline(false);
            LayerBreakable(false);
        }
    }

    public void ToggleOutline (bool enable)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (enable)
        {
            materials[1] = outline;
        }
        else
        {
            materials[1] = baseMat;
        }
        renderer.materials = materials;
        
    }

    public void LayerBreakable (bool enable) //Adds breakable objects to be seen through walls
    {
        if (enable)
        {
            gameObject.layer = 7;
        }
        else
        {
            gameObject.layer = 0;
        }
    }
}
