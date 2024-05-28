using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private ObjectRandomizer randomizerScript;

    [Header("Direction")]
    [Space(2)]
    public bool isNorth = false;
    public bool isEast = false;
    public bool isSouth = false;
    public bool isWest = false;
    [Space(2)]

    [Header("Destroy on Spawn")]
    [Space(2)]
    [Tooltip("Will destroy spawner on game start")]
    public bool destroy = false;

    private void Awake()
    {
        //Finds the generate Object object within a scene and references the generator script off of it 
        GameObject ObjectGeneratorObject = GameObject.Find("ObjectGenerator");
        randomizerScript = ObjectGeneratorObject.GetComponent<ObjectRandomizer>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //Determines the direction the generated object should face 
        if (isNorth == true)
        {
            randomizerScript.Generate(transform.position, new Vector3(0, 180, 0));

            if (destroy == true)
            {
                Destroy(gameObject);
            }
        }
        if (isSouth == true)
        {
            randomizerScript.Generate(transform.position, new Vector3(0, 0, 0));

            if (destroy == true)
            {
                Destroy(gameObject);
            }
        }
        if (isEast == true)
        {
            randomizerScript.Generate(transform.position, new Vector3(0, -90, 0));

            if (destroy == true)
            {
                Destroy(gameObject);
            }
        }
        if (isWest == true)
        {
            randomizerScript.Generate(transform.position, new Vector3(0, 90, 0));

            if (destroy == true)
            {
                Destroy(gameObject);
            }
        }
    }
}

