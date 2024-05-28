using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlipperThrow : MonoBehaviour
{

    //Values for throw charging
    [SerializeField]int framesSinceCharging = 0;
    public int StrongChargeFrames;
    public int NormalChargeFrames;
    public float StrongThrowForce;
    public float NormalThrowForce;
    public float WeakThrowForce;

    public GameObject throwObject;
    public GameObject throwOriginPoint;

    bool buttonHeld;

    public GameObject child;

    enum ShootType { Weak, Normal, Strong }
    ShootType shootType;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Checks for button down and starts charging
        if (Input.GetButtonDown("Throw"))
        {
            buttonHeld = true;
        }

        //checks for button released and triggers throw at appropriate strength
        if (Input.GetButtonUp("Throw"))
        {
            buttonHeld = false;
            if (framesSinceCharging > StrongChargeFrames) { shootType = ShootType.Strong; }
            else if (framesSinceCharging > NormalChargeFrames) { shootType = ShootType.Normal; }
            else { shootType = ShootType.Weak; }
            Throw(shootType);
            framesSinceCharging = 0;
        }

        if(buttonHeld)
        {
            framesSinceCharging++;
        }
    }

    void Throw(ShootType strength)
    {
        

        float ThrowSpeed =0;

        GameObject newObject = Instantiate(throwObject, throwOriginPoint.transform.position, Quaternion.identity);
        if(strength == ShootType.Weak)
        {
            ThrowSpeed = WeakThrowForce;
            
        }
        else if(strength == ShootType.Normal)
        {
            ThrowSpeed = NormalThrowForce;
        }
        else if (strength == ShootType.Strong)
        {
            ThrowSpeed = StrongThrowForce;
        }

        Debug.Log(ThrowSpeed);

        newObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * ThrowSpeed, ForceMode.Impulse);
    }


}