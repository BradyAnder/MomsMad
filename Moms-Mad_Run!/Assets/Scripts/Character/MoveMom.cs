using UnityEngine;
using UnityEngine.InputSystem;

public class MoveMom : MonoBehaviour
{
    public GameObject Mom;
    public float maxSpeed = 5f;
    public float acceleration = 20f; // 增加加速度
    public float deceleration = 20f; // 增加减速度
    public float mom_rotationSpeed = 10f;

    private Rigidbody mom;
    private Vector2 moveInput;
    private Vector3 currentVelocity = Vector3.zero;

    void Start()
    {
        if (Mom != null)
        {
            mom = Mom.GetComponent<Rigidbody>();
            mom.useGravity = true;
        }
    }

    void Update()
    {
        if (mom != null)
        {
            MoveMomObject();
        }
    }

    void MoveMomObject()
    {
        Vector3 targetVelocity = new Vector3(moveInput.x, 0.0f, moveInput.y) * maxSpeed;

        if (targetVelocity != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetVelocity);
            mom.rotation = Quaternion.Lerp(mom.rotation, targetRotation, mom_rotationSpeed * Time.deltaTime);
        }

        if (targetVelocity.magnitude > currentVelocity.magnitude)
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, acceleration * Time.deltaTime);
        }
        else
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, deceleration * Time.deltaTime);
        }

        mom.velocity = new Vector3(currentVelocity.x, mom.velocity.y, currentVelocity.z);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
