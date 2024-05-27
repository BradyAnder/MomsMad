using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectInventory : MonoBehaviour
{
    // Start is called before the first frame update

    private Inventory inventory;

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventory.SetSelectedIndex(0);
        }
    }
}
