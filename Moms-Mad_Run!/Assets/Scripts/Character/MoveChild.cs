using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MoveChild : MonoBehaviour
{
    public GameObject Child_Object;
    public float child_speed = 8f;
    public float child_rotationSpeed = 10f;
    private Rigidbody child_body;
    

    private Vector2 child_move = Vector2.zero;
    public PlayerControls playerControls;
    private InputAction Movement;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.ChildMovementControl.Move.performed += ctx => child_move = ctx.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        Movement = playerControls.ChildMovementControl.Move;
        Movement.Enable();
    }

    private void OnDisable()
    {
        Movement.Disable();
    }


  
    // Start is called before the first frame update
    void Start()
    {
        if (Child_Object != null)
        {
            child_body = Child_Object.GetComponent<Rigidbody>();
            child_body.useGravity = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (child_body != null)
        {
            MoveChildObject();
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
}