using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunPlayer : MonoBehaviour
{

    public GetStunned stun;
    public float stunTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && stun.canBeStunned == true)
        {
            Debug.Log("Stun");
            StartCoroutine(Stun());
        }
    }

    private IEnumerator Stun()
    {
        stun.StunPlayer();
        yield return new WaitForSecondsRealtime(stunTime);
        stun.UnstunPlayer();
        Debug.Log("Stun Finished");
    }
}
