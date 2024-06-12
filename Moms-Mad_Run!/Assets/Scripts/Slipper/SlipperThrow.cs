using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlipperThrow : MonoBehaviour
{
    // Values for throw charging
    [SerializeField] int framesSinceCharging = 0;
    public int StrongChargeFrames;
    public int NormalChargeFrames;
    public float StrongThrowForce = 30;
    public float NormalThrowForce = 22;
    public float WeakThrowForce = 15;

    public float strongChargeTime = 1.6f;
    public float normalChargeTime = 0.8f;
    private float currChargeTime = 0;
    public float reloadTime = 0.8f;
    private float currReloadTime = 0;
    private bool isReadyThrow = true;

    public GameObject throwObject;
    public GameObject throwOriginPoint;

    bool buttonHeld = false;

    public GameObject child;

    enum ShootType { Weak, Normal, Strong }
    ShootType shootType;

    void Update()
    {
        if (buttonHeld)
        {
            currChargeTime += Time.deltaTime;
        }

        if (!isReadyThrow)
        {
            currReloadTime += Time.deltaTime;
            if (currReloadTime >= reloadTime)
            {
                isReadyThrow = true;
            }
            return;
        }
    }

    void Throw(ShootType strength)
    {
        float ThrowSpeed = 0;

        GameObject newObject = Instantiate(throwObject, throwOriginPoint.transform.position, Quaternion.identity);
        switch (strength)
        {
            case ShootType.Weak:
                ThrowSpeed = WeakThrowForce;
                break;
            case ShootType.Normal:
                ThrowSpeed = NormalThrowForce;
                break;
            case ShootType.Strong:
                ThrowSpeed = StrongThrowForce;
                break;
            default:
                break;
        }

        Debug.Log(ThrowSpeed);

        newObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * ThrowSpeed, ForceMode.Impulse);
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            buttonHeld = true;
        }
        else if (context.canceled)
        {
            buttonHeld = false;
            if (!isReadyThrow) { return; }
            if (currChargeTime > strongChargeTime) { shootType = ShootType.Strong; }
            else if (currChargeTime > normalChargeTime) { shootType = ShootType.Normal; }
            else { shootType = ShootType.Weak; }
            Throw(shootType);
            currChargeTime = 0;
            isReadyThrow = false;
            currReloadTime = 0;
        }
    }
}
