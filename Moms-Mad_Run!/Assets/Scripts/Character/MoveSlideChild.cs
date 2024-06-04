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

    private Rigidbody child_body;
    public bool isSliding = false;
    private float slideTimer = 0f;
    private float slideCooldownTimer = 0f;
    private Vector3 slideDirection;
    private Quaternion originalRotation;
    private CapsuleCollider childCollider;
    private Vector2 moveInput;
    
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
        slideTimer -= Time.deltaTime;
        if (slideTimer <= 0)
        {
            EndSlide();
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
