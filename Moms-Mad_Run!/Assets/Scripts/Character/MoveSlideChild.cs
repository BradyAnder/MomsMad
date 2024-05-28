using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSlideChild : MonoBehaviour
{
    public GameObject Child_Object;
    public float child_speed = 8f;
    public float child_rotationSpeed = 10f;
    public float slideSpeed = 20f;
    public float slideDuration = 0.5f;
    public float slideCooldown = 1f;
    // Offset for the child's hitbox when sliding (lowered to the ground)
    public Vector3 slideHitboxOffset = new Vector3(0f, -0.5f, 0f); 

    private Rigidbody child_body;
    private bool isSliding = false;
    private float slideTimer = 0f;
    private float slideCooldownTimer = 0f;
    private Vector3 originalHitboxCenter;
    private BoxCollider childCollider;

    // Start is called before the first frame update
    void Start()
    {
        if (Child_Object != null)
        {
            child_body = Child_Object.GetComponent<Rigidbody>();
            child_body.useGravity = true;
            
            // Assuming the child object has a BoxCollider component for hitbox in "Face" component.
            childCollider = Child_Object.GetComponentInChildren<BoxCollider>();
            if (childCollider != null)
            {
                
                originalHitboxCenter = childCollider.center;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (child_body != null)
        {
            if (isSliding)
            {
                Slide();
            }
            else
            {
                MoveChildObject();
            }

            if (slideCooldownTimer > 0)
            {
                slideCooldownTimer -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.Space) && !isSliding && slideCooldownTimer <= 0)
            {
                StartSlide();
            }
        }
    }

    void MoveChildObject()
    {
        float child_moveHorizontal = 0f;
        float child_moveVertical = 0f;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            child_moveVertical = 1f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            child_moveVertical = -1f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            child_moveHorizontal = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            child_moveHorizontal = 1f;
        }

        Vector3 movement_child = new Vector3(child_moveHorizontal, 0.0f, child_moveVertical);
        
        if (movement_child != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement_child);
            child_body.rotation = Quaternion.Lerp(child_body.rotation, targetRotation, child_rotationSpeed * Time.deltaTime);
        }

        child_body.velocity = new Vector3(movement_child.x * child_speed, child_body.velocity.y, movement_child.z * child_speed);
    }

    void StartSlide()
    {
        isSliding = true;
        slideTimer = slideDuration;
        slideCooldownTimer = slideCooldown;

        if (childCollider != null)
        {
            childCollider.center += slideHitboxOffset;
        }

        child_body.velocity = Child_Object.transform.forward * slideSpeed;
    }

    void Slide()
    {
        slideTimer -= Time.deltaTime;
        if (slideTimer <= 0)
        {
            EndSlide();
        }
    }

    void EndSlide()
    {
        isSliding = false;
        if (childCollider != null)
        {
            childCollider.center = originalHitboxCenter;
        }
    }
}
