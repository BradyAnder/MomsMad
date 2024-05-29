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
            Debug.Log("Mom won!");
            SceneManager.LoadScene("Lose");

            // Add displaying a UI message, etc.
        }
    }
}
