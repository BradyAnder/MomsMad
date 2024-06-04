using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectRandomizer: MonoBehaviour
{

    //List of all object that can be generated 
    public List<GameObject> objectList = new List<GameObject>();

    //Generates a random object from a list and generates that object at a Target position and Target rotation 
    public void Generate(Vector3 targetPosition, Vector3 targetRotation)
    {
        int randomLandmark = Random.Range(0, objectList.Count);
        Instantiate(objectList[randomLandmark], targetPosition, Quaternion.Euler(targetRotation));
        objectList.RemoveAt(randomLandmark);
    }
}