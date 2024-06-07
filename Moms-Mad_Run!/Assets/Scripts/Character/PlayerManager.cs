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

        if (playerInput.name == "Child" && playerInput.playerIndex == 0 )
        {
            playerInput.gameObject.SetActive(false);
        }
        else if(playerInput.name == "Mom" && playerInput.playerIndex > 0)
        {
            playerInput.gameObject.SetActive(false);
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

    public void PlayerJoined()
    {

    }
}