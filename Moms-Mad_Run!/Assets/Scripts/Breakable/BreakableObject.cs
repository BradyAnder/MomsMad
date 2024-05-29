using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject brokenObject;
    private MoveSlideChild moveScript;

    private void Start()
    {
        GameObject childObject = GameObject.Find("Child");
        moveScript = childObject.GetComponent<MoveSlideChild>();
    }


    void BreakObject()
    {
        Instantiate(brokenObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && moveScript.isSliding == true)
        {
            BreakObject();
        }
    }
}
