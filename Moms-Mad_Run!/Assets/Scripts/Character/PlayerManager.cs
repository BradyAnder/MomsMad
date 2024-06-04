using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerPrefab;

    void Start()
    {
        PlayerInputManager.instance.onPlayerJoined += OnPlayerJoined;
    }

    void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("Player joined: " + playerInput.playerIndex);

        // Assuming the playerPrefab contains mom and child as children
        Transform momTransform = playerInput.transform.Find("Mom");
        Transform childTransform = playerInput.transform.Find("Child");

        if (momTransform != null && playerInput.playerIndex == 0)
        {
            SetupMom(momTransform.GetComponent<PlayerInput>());
        }

        if (childTransform != null && playerInput.playerIndex == 1)
        {
            SetupChild(childTransform.GetComponent<PlayerInput>());
        }
    }

    void SetupMom(PlayerInput momInput)
    {
        // Setup specific to mom
        momInput.SwitchCurrentActionMap("MomActions");
    }

    void SetupChild(PlayerInput childInput)
    {
        // Setup specific to child
        childInput.SwitchCurrentActionMap("ChildActions");
    }
}