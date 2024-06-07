using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

    public GameObject FirstButton;

    // Start is called before the first frame update
    void Start()
    {
        //sets which button starts selected
        EventSystem.current.SetSelectedGameObject(FirstButton);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
