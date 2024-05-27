using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    public Inventory inventory;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    public void Interact()
    {
        if (inventory.isFull[0] == true)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(2);
        }
    }
}
