using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;

    public int selectedIndex = -1;


    public void Update()
    {
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        if (scroll > 0)
        {
            SelectPrevSlot();
        }
        if (scroll < 0)
        {
            SelectNextSlot();
        }
    }

    private void SelectNextSlot()
    {
        selectedIndex++;
        if (selectedIndex >= slots.Length)
        {
            selectedIndex = 0;
        }
    }

    private void SelectPrevSlot()
    {
        selectedIndex--;
        if (selectedIndex < 0)
        {
            selectedIndex = slots.Length - 1;
        }
    }


    public void SetSelectedIndex(int index)
    {
        selectedIndex = index;
    }

    public int GetSelectedIndex()
    {
        return selectedIndex;
    }
}
