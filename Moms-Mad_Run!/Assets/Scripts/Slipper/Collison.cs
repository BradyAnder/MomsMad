using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlipperTrigger : MonoBehaviour
{
    public float minStunSpeed = 5.0f;
    private Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (rb == null)
            return;
        if (other.gameObject.tag == "Player" && rb.velocity.magnitude > minStunSpeed)
        {
            StartCoroutine(StunPlayer(other.gameObject));
            // Add displaying a UI message, etc.
        }
    }

    // Here we are using a coroutine to stun the player for 2 seconds 
    private IEnumerator StunPlayer(GameObject player)
    {

        
        MoveSlideChild moveChild = player.GetComponent<MoveSlideChild>();

        // Debug.Log(moveChild);
        if (moveChild != null)
        {
            moveChild.enabled = false;
            Debug.Log("onhit" + moveChild.enabled);
            yield return new WaitForSeconds(0.8f);
            moveChild.enabled = true;
            Debug.Log("resume" + moveChild.enabled);
        }
    }
}
