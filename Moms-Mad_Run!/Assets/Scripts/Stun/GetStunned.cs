using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GetStunned : MonoBehaviour
{
    public PlayerInput playerInput;
    public bool canBeStunned;
    public GameObject stunEffect;
    public float stunResistance;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.enabled = true;
        canBeStunned = true;
    }

    public void StunTest()
    {
        StartCoroutine(Stun());
    }

    public void StunPlayer()
    {
        playerInput.enabled = false;
        Instantiate(stunEffect, this.transform);
    }

    public void UnstunPlayer()
    {
        playerInput.enabled = true;
        StartCoroutine(StunCooldown());
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            var child = transform.GetChild(i);
            if (child.CompareTag("Effect"))
            {
                Object.Destroy(child.gameObject);
            }
        }
    }

    private IEnumerator StunCooldown()
    {
        yield return new WaitForSecondsRealtime(stunResistance);
        canBeStunned = true;
    }

    public IEnumerator Stun()
    { 
            StunPlayer();
            Debug.Log("Wating");
            yield return new WaitForSecondsRealtime(6);
            Debug.Log("more waiting");
            UnstunPlayer();
            Debug.Log("Unstun");
        
    }
}
