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
        MoveChild moveChild = player.GetComponent<MoveChild>();
        if (moveChild != null)
        {
            moveChild.enabled = false;
            yield return new WaitForSeconds(2.0f);
            moveChild.enabled = true;
        }
        
    }
}
