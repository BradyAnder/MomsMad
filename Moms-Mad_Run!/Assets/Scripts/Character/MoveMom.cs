using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public GameObject Mom;
    public float mom_speed = 5f;
    public float mom_rotationSpeed = 10f;

    private Rigidbody mom;

    // Start is called before the first frame update
    void Start()
    {
        if (Mom != null)
        {
            mom = Mom.GetComponent<Rigidbody>();
            mom.useGravity = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mom != null)
        {
            MoveMom();
        }
    }

    void MoveMom()
    {
        float mom_moveHorizontal = Input.GetAxis("Horizontal");
        float mom_moveVertical = Input.GetAxis("Vertical");

        Vector3 movement_mom = new Vector3(mom_moveHorizontal, 0.0f, mom_moveVertical);
        if (movement_mom != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement_mom);
            mom.rotation = Quaternion.Lerp(mom.rotation, targetRotation, mom_rotationSpeed * Time.deltaTime);
        }

        mom.velocity = new Vector3(movement_mom.x * mom_speed, mom.velocity.y, movement_mom.z * mom_speed);
    }

}
