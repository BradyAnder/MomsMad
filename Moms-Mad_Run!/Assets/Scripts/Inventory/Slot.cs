using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Inventory inventory;
    public int slotIndex;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHighlight();

        // Check for item dropping and usage
        if (Input.GetKeyDown(KeyCode.Q) && inventory.GetSelectedIndex() == slotIndex)
        {
            DropItem();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && inventory.GetSelectedIndex() == slotIndex)
        {
            UseItem();
        }
    }
    void UpdateHighlight()
    {
        if (inventory.GetSelectedIndex() == slotIndex)
        {
            GetComponent<Image>().color = Color.gray;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }
    }

    public void DropItem()
    {
        if (Input.GetKeyDown(KeyCode.Q) && inventory.GetSelectedIndex() == slotIndex)
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<Spawn>().SpawnDroppedItem();

                GameObject.Destroy(child.gameObject);

                inventory.isFull[slotIndex] = false;
            }
        }
    }

    public void UseItem()
    {
        if (transform.childCount > 0)
        {
            Transform item = transform.GetChild(0);
            IItems itemComponent = item.GetComponent<IItems>();

            // Check if the item implements IItem interface
            if (itemComponent != null)
            {
                itemComponent.Use(); // Use the item
                GameObject.Destroy(item.gameObject);
                inventory.isFull[slotIndex] = false;
            }
            else
            {
                Debug.LogWarning("Selected item does not implement IItem interface.");
            }
        }
    }
}
