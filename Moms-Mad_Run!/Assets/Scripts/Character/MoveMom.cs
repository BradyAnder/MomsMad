using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    public GameObject Mom;
    public float mom_speed = 5f;
    public float mom_rotationSpeed = 10f;

    private Rigidbody mom;
    private Vector2 moveInput;

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
            MoveMom();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void MoveMom()
    {
        Vector3 movement_mom = new Vector3(moveInput.x, 0.0f, moveInput.y);
        if (movement_mom != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement_mom);
            mom.rotation = Quaternion.Lerp(mom.rotation, targetRotation, mom_rotationSpeed * Time.deltaTime);
        }

        mom.velocity = new Vector3(movement_mom.x * mom_speed, mom.velocity.y, movement_mom.z * mom_speed);
    }
}
