using UnityEngine;
using UnityEngine.InputSystem;

public class MoveSlideChild : MonoBehaviour
{
    public GameObject Child_Object;
    public float child_speed = 8f;
    public float child_rotationSpeed = 10f;
    public float slideSpeed = 20f;
    public float slideDuration = 0.5f;
    public float slideCooldown = 1f;
    public float rotationSpeed = 100f;

    private Rigidbody child_body;
    public bool isSliding = false;
    private float slideTimer = 0f;
    public float slideCooldownTimer = 0f;
    private Vector3 slideDirection;
    private Quaternion originalRotation;
    private CapsuleCollider childCollider;
    private Vector2 moveInput;
    private float slideAngle;
    private Vector3 newSlideDirection;

    void Start()
    {
        if (Child_Object != null)
        {
            child_body = Child_Object.GetComponent<Rigidbody>();
            child_body.useGravity = true;

            childCollider = Child_Object.GetComponent<CapsuleCollider>();
            if (childCollider != null)
            {
                originalRotation = Child_Object.transform.localRotation;
            }
        }
    }

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
        }

    }

    void MoveChildObject()
    {
        Vector3 movement_child = new Vector3(moveInput.x, 0.0f, moveInput.y);

        if (movement_child != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement_child);
            child_body.rotation = Quaternion.Lerp(child_body.rotation, targetRotation, child_rotationSpeed * Time.deltaTime);
        }

        child_body.velocity = new Vector3(movement_child.x * child_speed, child_body.velocity.y, movement_child.z * child_speed);
    }

    void StartSlide()
    {
        if (!isSliding && slideCooldownTimer <= 0)
        {
            isSliding = true;
            slideTimer = slideDuration;
            slideCooldownTimer = slideCooldown;

            // Store the original direction before rotating the character
            slideDirection = Child_Object.transform.forward;

            // Rotate the character to look at the ceiling
            Child_Object.transform.Rotate(-90f, 0f, 0f);

            // Apply the sliding velocity in the original forward direction
            child_body.velocity = slideDirection * slideSpeed;
        }
    }

void Slide()
{
    if (!isSliding && slideCooldownTimer <= 0)
    {
        // Start the slide
        isSliding = true;
        slideTimer = slideDuration;
        slideCooldownTimer = slideCooldown;

        // Store the original direction before rotating the character
        slideDirection = Child_Object.transform.forward;

        // Rotate the character to look at the ceiling
        Child_Object.transform.Rotate(-90f, 0f, 0f);

        // Ensure the player moves in the direction they are facing even if standing still
        if (child_body.velocity.magnitude == 0)
        {
            child_body.velocity = slideDirection * slideSpeed;
        }
        else
        {
            // Apply the sliding velocity in the original forward direction
            child_body.velocity = slideDirection * slideSpeed;
        }
    }

    if (isSliding)
    {
        slideTimer -= Time.deltaTime;

        if (slideTimer <= 0)
        {
            // End the slide
            isSliding = false;
            child_body.velocity = Vector3.zero;

            // Reset the rotation of the character to its original state
            Child_Object.transform.Rotate(90f, 0f, 0f);
        }
        else
        {
                

            //Checking slide direciton after rotation
            newSlideDirection = -1 *Child_Object.transform.up;
            
                //change moveinput to work in 3d
                Vector3 moveInput3 = new Vector3(moveInput.x, 0f, moveInput.y);

                // Check how large the angle between stick direction and player facing direciton
                slideAngle = Vector3.Angle(newSlideDirection, moveInput3);

            // Check direction pressed and invert angle if required
            Vector3 cross = Vector3.Cross(newSlideDirection, moveInput3);
                if( cross.y < 0)
                {
                    slideAngle = -slideAngle;
                }


            // Rotate the child object based on the horizontal input to face the direction of the user input
            Vector3 rotation = new Vector3(0.0f, 0.0f, slideAngle);
            Child_Object.transform.Rotate(rotation * rotationSpeed * Time.deltaTime);


                // Maintain sliding direction but allow slight steering left and right
                Vector3 movement = newSlideDirection * slideSpeed;
            child_body.velocity = movement.normalized * slideSpeed;
        }
    }
}
    void EndSlide()
    {
        isSliding = false;

        // Reset the character's rotation back to the original
        Child_Object.transform.localRotation = originalRotation;

        // Stop the sliding movement
        child_body.velocity = Vector3.zero;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnSlide(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartSlide();
        }
    }
}
