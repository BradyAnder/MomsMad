using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableEffects : MonoBehaviour
{
    public GameObject effectSpawn;
    public ParticleSystem particleFX;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(particleFX, effectSpawn.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
