using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public TextMeshProUGUI controllerCountText;

    void Start()
    {
        UpdateControllerCount();
    }

    void Update()
    {
        UpdateControllerCount();
    }

    void UpdateControllerCount()
    {
        int controllerCount = 0;
        string[] joysticks = Input.GetJoystickNames();
        foreach (string joystick in joysticks)
        {
            if (!string.IsNullOrEmpty(joystick))
            {
                controllerCount++;
            }
        }

        controllerCountText.text = "Controllers Connected: " + controllerCount;
    }
}
