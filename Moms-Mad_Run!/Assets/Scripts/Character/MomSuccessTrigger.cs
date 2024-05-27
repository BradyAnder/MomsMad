using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MomSuccessTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject child;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == child)
        {
            Debug.Log("Mom won!");
            
            // Add displaying a UI message, etc.
        }
    }
}
