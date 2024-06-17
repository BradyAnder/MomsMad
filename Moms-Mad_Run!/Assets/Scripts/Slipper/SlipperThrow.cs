using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlipperThrow : MonoBehaviour
{
    // Values for throw charging
    public float StrongThrowForce = 30;
    public float NormalThrowForce = 22;
    public float WeakThrowForce = 15;

    public float strongChargeTime = 1.6f;
    public float normalChargeTime = 0.8f;
    private float currChargeTime = 0;
    public float reloadTime = 0.8f;
    private float currReloadTime = 0;
    private bool isReadyThrow = true;

    public float PowerUpTime = 5.0f;
    private float currPowerUpTime = 0;
    private bool isPowerUp = false;
    private PowerUpMode powerUpMode;
    private float reloadSMGTime = 0.1f;
    private float currSMGTime = 0;
    private bool isSMGReady = true;


    public GameObject throwObject;
    public GameObject throwOriginPoint;

    bool buttonHeld = false;

    public GameObject child;
    

    enum ShootType { Weak, Normal, Strong }
    ShootType shootType;

    private void Start()
    {
        ///// Powerup Test Script ////
        // PowerUpTime = 10.0f;
        // powerUpMode = PowerUpMode.FanShape;
        // isPowerUp = true;
        //////////////////////////////
    }
    void Update()
    {
        if (buttonHeld)
        {
            if (isPowerUp && powerUpMode.Equals(PowerUpMode.SMG))
            {
                if (isSMGReady)
                {
                    Throw(ShootType.Normal);
                    isSMGReady = false;
                }
                else {
                    currSMGTime += Time.deltaTime;
                }
                if (currSMGTime >= reloadSMGTime) {
                    currSMGTime = 0;
                    isSMGReady = true;
                }
                currChargeTime = 0;
            }
            else
            {
                currChargeTime += Time.deltaTime;
            }
        }
        if (!isReadyThrow)
        {
            currReloadTime += Time.deltaTime;
            if (currReloadTime >= reloadTime)
            {
                isReadyThrow = true;
            }
        }
        if (isPowerUp) {
            currPowerUpTime += Time.deltaTime;
            Debug.Log(currPowerUpTime);
            if (currPowerUpTime >= PowerUpTime) {
                Debug.Log("Powerup Ends");
                currPowerUpTime = 0;
                isPowerUp = false;
            }
        }
    }

    void Throw(ShootType strength)
    {
        if (!isPowerUp)
        {
            float ThrowSpeed = 0;
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
            GameObject newObject = Instantiate(throwObject, throwOriginPoint.transform.position, Quaternion.identity);
            Physics.IgnoreCollision(newObject.GetComponent<Collider>(), GetComponent<Collider>());
            newObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * ThrowSpeed, ForceMode.Impulse);
            return;
        }
        switch (powerUpMode) {
            case (PowerUpMode.Bouncy):
                GameObject newObject = Instantiate(throwObject, throwOriginPoint.transform.position, Quaternion.identity);
                Physics.IgnoreCollision(newObject.GetComponent<Collider>(), GetComponent<Collider>());
                SlipperPhysics slipperPhysics = newObject.GetComponent<SlipperPhysics>();
                slipperPhysics.bounce = 0.8f;
                newObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * NormalThrowForce, ForceMode.Impulse);
                return;
            case (PowerUpMode.FanShape):
                GameObject newObject1 = Instantiate(throwObject, throwOriginPoint.transform.position, Quaternion.identity);
                GameObject newObject2 = Instantiate(throwObject, throwOriginPoint.transform.position, Quaternion.identity);
                GameObject newObject3 = Instantiate(throwObject, throwOriginPoint.transform.position, Quaternion.identity);
                Vector3 leftdir = Quaternion.Euler(0, -30, 0) * transform.forward;
                Vector3 rightdir = Quaternion.Euler(0, 30, 0) * transform.forward;
                Physics.IgnoreCollision(newObject1.GetComponent<Collider>(), GetComponent<Collider>());
                Physics.IgnoreCollision(newObject2.GetComponent<Collider>(), GetComponent<Collider>());
                Physics.IgnoreCollision(newObject3.GetComponent<Collider>(), GetComponent<Collider>());
                newObject1.GetComponent<Rigidbody>().AddForce(leftdir * NormalThrowForce, ForceMode.Impulse);
                newObject2.GetComponent<Rigidbody>().AddForce(this.transform.forward * NormalThrowForce, ForceMode.Impulse);
                newObject3.GetComponent<Rigidbody>().AddForce(rightdir * NormalThrowForce, ForceMode.Impulse);
                return;
            case (PowerUpMode.SMG):
                GameObject newObject4 = Instantiate(throwObject, throwOriginPoint.transform.position, Quaternion.identity);
                Physics.IgnoreCollision(newObject4.GetComponent<Collider>(), GetComponent<Collider>());
                newObject4.GetComponent<Rigidbody>().AddForce(this.transform.forward * NormalThrowForce, ForceMode.Impulse);
                return;
            default:
                return;
        }

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
            return;
        }
    }

    public void GetPowerUp(PowerUpMode mode) {
        if (isPowerUp) {
            currPowerUpTime = 0;
        }
        powerUpMode = mode;
        isPowerUp = true;
        return;
    }

}
