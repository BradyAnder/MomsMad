using UnityEngine;
using UnityEngine.InputSystem;
public class ControllerManager : MonoBehaviour
{
    PlayerInput playerInput;
    InputAction movement;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        movement = playerInput.actions["Move"];
    }

    void Update()
    {
        Vector2 move = movement.ReadValue<Vector2>();
        Debug.Log(move);
    }

    void OnEnable()
    {
        movement.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
    }




}