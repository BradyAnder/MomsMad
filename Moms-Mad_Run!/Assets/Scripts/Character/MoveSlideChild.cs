using UnityEngine;
using UnityEngine.InputSystem;

public class MoveSlideChild : MonoBehaviour
{
    public GameObject Child_Object;
    public float maxSpeed = 8f;
    public float acceleration = 20f; // 增加加速度
    public float deceleration = 20f; // 增加减速度
    public float child_rotationSpeed = 10f;
    public float slideSpeed = 20f;
    public float slideDuration = 0.5f;
    public float slideCooldown = 1f;

    private Rigidbody child_body;
    public bool isSliding = false;  // 修改为 public
    private float slideTimer = 0f;
    private float slideCooldownTimer = 0f;
    private Vector3 slideDirection;
    private Quaternion originalRotation;
    private CapsuleCollider childCollider;
    private Vector2 moveInput;
    private Vector3 currentVelocity = Vector3.zero;

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
        Vector3 targetVelocity = new Vector3(moveInput.x, 0.0f, moveInput.y) * maxSpeed;

        if (targetVelocity != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetVelocity);
            child_body.rotation = Quaternion.Lerp(child_body.rotation, targetRotation, child_rotationSpeed * Time.deltaTime);
        }

        if (targetVelocity.magnitude > currentVelocity.magnitude)
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, acceleration * Time.deltaTime);
        }
        else
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, deceleration * Time.deltaTime);
        }

        child_body.velocity = new Vector3(currentVelocity.x, child_body.velocity.y, currentVelocity.z);
    }

    void StartSlide()
    {
        if (!isSliding && slideCooldownTimer <= 0)
        {
            isSliding = true;
            slideTimer = slideDuration;
            slideCooldownTimer = slideCooldown;

            slideDirection = Child_Object.transform.forward;

            Child_Object.transform.Rotate(-90f, 0f, 0f);

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

        Child_Object.transform.localRotation = originalRotation;

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
