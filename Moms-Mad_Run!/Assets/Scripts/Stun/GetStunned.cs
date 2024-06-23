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

    public void StunPlayer()
    {
        playerInput.enabled = false;
        canBeStunned = false;
        Instantiate(stunEffect, transform.position, transform.rotation);
    }

    public void UnstunPlayer()
    {
        playerInput.enabled = true;
        StartCoroutine(StunCooldown());
    }

    private IEnumerator StunCooldown()
    {
        yield return new WaitForSecondsRealtime(stunResistance);
        canBeStunned = true;
    }
}
