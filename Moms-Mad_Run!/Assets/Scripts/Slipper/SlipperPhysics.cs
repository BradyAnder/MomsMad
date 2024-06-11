using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperPhysics : MonoBehaviour
{
    public short maxBounceNumber = 10;
    private short currBounceCount = 0;
    public float maxAliveSeconds = 3.0f;
    private float currAliveSeconds = 0;
    public float bounce = 0.5f;
    private Vector3 currVelocity;
    private Rigidbody rb;
    public Vector3 initAngularV = new Vector3(540, 0, 0);


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.angularVelocity = initAngularV;
    }
    private void Update()
    {
        currVelocity = rb.velocity;
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
            // Debug.Log("Slipper reaches maximum bounces");
            Destroy(gameObject);
        }
        else {
            if (rb == null)
            {
                // Debug.Log("Slipper missing rigidbody comp");
                return;
            }

            Vector3 collisionNormal = collision.contacts[0].normal;
            Vector3 reflectedVelocity = Vector3.Reflect(currVelocity, collisionNormal) * bounce;
            //reflectedVelocity.y *= 0.2f;
            rb.velocity = reflectedVelocity;
        }
    }
}
