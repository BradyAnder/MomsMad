using UnityEngine;
using UnityEngine.InputSystem;

public class MoveChild : MonoBehaviour
{
    public GameObject Child_Object;
    public float child_speed = 8f;
    public float child_rotationSpeed = 10f;
    private Rigidbody child_body;
    private Vector2 moveInput;

    void Start()
    {
        if (Child_Object != null)
        {
            child_body = Child_Object.GetComponent<Rigidbody>();
            child_body.useGravity = true;
        }
    }

    void Update()
    {
        if (child_body != null)
        {
            MoveChildObject();
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

    public void StunChild()
    {
        StartCoroutine(StunChildCoroutine());
    }

    private IEnumerator StunChildCoroutine()
    {
        child_body.useGravity = false;
        child_body.velocity = Vector3.zero;
        yield return new WaitForSeconds(2.0f);
        child_body.useGravity = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
