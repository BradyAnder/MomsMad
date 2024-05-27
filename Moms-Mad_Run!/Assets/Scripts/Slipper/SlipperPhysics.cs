using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperPhysics : MonoBehaviour
{
    public short maxBounceNumber = 10;
    private short currBounceCount = 0;
    public float maxAliveSeconds = 3.0f;
    private float currAliveSeconds = 0;
    public float bounceForceMagnitude = 1.0f;
    private Rigidbody rb;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        currAliveSeconds += Time.deltaTime;
        if (currAliveSeconds >= maxAliveSeconds) {
            // Debug.Log("Slipper reaches maximum alive time");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Mom"))
            return;
        if (++currBounceCount >= maxBounceNumber)
        {
            Debug.Log("Slipper reaches maximum bounces");
            Destroy(gameObject);
        }
        else {
            if (rb == null)
            {
                Debug.Log("Slipper missing rigidbody comp");
                return;
            }
            Vector3 forceDirection = -collision.contacts[0].normal;
            rb.AddForce(forceDirection * bounceForceMagnitude, ForceMode.Impulse);
        }
    }
}
