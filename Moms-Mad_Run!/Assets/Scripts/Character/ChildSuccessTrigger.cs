using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSuccessTrigger : MonoBehaviour
{
    public GameObject child; // Reference to the child object
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == child)
        {
            Debug.Log("Child Won");
            // showing a success UI, etc.
        }
    }
}
