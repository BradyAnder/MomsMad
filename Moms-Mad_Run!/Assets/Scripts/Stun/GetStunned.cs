using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GetStunned : MonoBehaviour
{
    //public PlayerInput playerInput;
    public MoveSlideChild moveSlideChild;
    public bool canBeStunned;
    public GameObject stunEffect;
    public float stunResistance;
    public GameObject shield;


    // Start is called before the first frame update
    void Start()
    {
        //playerInput = GetComponent<PlayerInput>();
        canBeStunned = true;
        moveSlideChild = GetComponent<MoveSlideChild>();
        moveSlideChild.enabled = true;
        //mat = GetComponent<Renderer>().material;
        //originalOpacity = mat.color.a;


    }

    public void StunTest()
    {
        StartCoroutine(Stun());
    }

    public void StunPlayer()
    {
        moveSlideChild.slideCooldownTimer += 6;
        moveSlideChild.enabled = false;
        Debug.Log("SLIDE");
        Instantiate(stunEffect, this.transform);
    }

    public void UnstunPlayer()
    {
        moveSlideChild.enabled = true;
        moveSlideChild.slideCooldownTimer = 0;
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            var child = transform.GetChild(i);
            if (child.CompareTag("Effect"))
            {
                Object.Destroy(child.gameObject);
            }
        }
        StartCoroutine(StunCooldown());

    }

    private IEnumerator StunCooldown()
    {
        Instantiate(shield, this.transform);
        yield return new WaitForSecondsRealtime(stunResistance);
        canBeStunned = true;
        for (var i = transform.childCount - 1; i >= 0; i--)
        {
            var child = transform.GetChild(i);
            if (child.CompareTag("Effect"))
            {
                Object.Destroy(child.gameObject);
            }
        }
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

    /*void FadeNow()
    {
        Color currentColor = mat.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, .2f, 2 * Time.deltaTime));
        mat.color = smoothColor;
    }

    void ResetFade()
    {
        Color currentColor = mat.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, originalOpacity, 2 * Time.deltaTime));
        mat.color = currentColor;
    }*/
}
