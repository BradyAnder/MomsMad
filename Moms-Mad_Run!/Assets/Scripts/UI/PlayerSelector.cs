using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelector : MonoBehaviour
{
    public int playerIndex; // The index of the player this selector belongs to
    public int currentSlotIndex = 0; // Start at the first slot

    public TextMeshProUGUI[] characterSlots; // Reference to the character slots

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        UpdateSelectorPosition(-1);
    }

    void Update()
    {
        if (Input.GetButtonDown("Horizontal"))
        {
            float input = Input.GetAxis("Horizontal");

            if (input > 0)
            {
                MoveRight();
            }
            else if (input < 0)
            {
                MoveLeft();
            }
        }
    }

    void MoveLeft()
    {
        if (currentSlotIndex > 0)
        {
            currentSlotIndex--;
            UpdateSelectorPosition(1);
        }
    }

    void MoveRight()
    {
        if (currentSlotIndex < characterSlots.Length - 1)
        {
            currentSlotIndex++;
            UpdateSelectorPosition(0);
        }
    }

    void UpdateSelectorPosition(int isLeft)
    {
        Vector3 position = characterSlots[currentSlotIndex].transform.position;
        
        position.x -= 70;
        position.y -= 150;
        characterSlots[currentSlotIndex].text = "Ready";

        if (isLeft == 1)
        {
            if (currentSlotIndex == 3)
            {
                characterSlots[currentSlotIndex + 1].text = "Join!\nMom";
            }
            else
            {
                characterSlots[currentSlotIndex + 1].text = "Join!\n Kid";
            }
            
        }
        else if (isLeft == 0)
        {
            characterSlots[currentSlotIndex - 1].text = "Join!\n Kid";
        }
        
        rectTransform.position = position;
    }
}
