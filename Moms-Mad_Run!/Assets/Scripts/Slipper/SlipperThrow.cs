using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlipperThrow : MonoBehaviour
{

    //Values for throw charging
    [SerializeField]int framesSinceCharging = 0;
    public int StrongChargeFrames;
    public int NormalChargeFrames;
    public float StrongThrowForce = 30;
    public float NormalThrowForce = 22;
    public float WeakThrowForce = 15;

    public bool useFrame = false;
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

    public PlayerControls playerControls;
    private InputAction ThrowAction;
    private Vector2 throwDirection = Vector2.zero;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.MomMovementControl.Throw.performed += ctx => throwDirection = ctx.ReadValue<Vector2>();
    }

    private void OnEnable()
    {
        ThrowAction = playerControls.MomMovementControl.Throw;
        ThrowAction.Enable();
    }

    private void OnDisable()
    {
        ThrowAction.Disable();
    }
   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!useFrame) { return; }
        if (Input.GetButtonDown("Throw"))
        {
            buttonHeld = true;
        }
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
        */
        if (!useFrame) { return; }
        if (Input.GetButtonDown("Throw") || throwDirection != Vector2.zero)
        {
            buttonHeld = true;
        }
        if (buttonHeld)
        {
            Debug.Log(currChargeTime);
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
        if (Input.GetButtonUp("Throw") || throwDirection == Vector2.zero)
        {
            buttonHeld = false;
            if (currChargeTime > strongChargeTime) { shootType = ShootType.Strong; }
            else if (currChargeTime > normalChargeTime) { shootType = ShootType.Normal; }
            else { shootType = ShootType.Weak; }
            Throw(shootType);
            currChargeTime = 0;
            isReadyThrow = false;
            currReloadTime = 0;
        }
    }

    private void FixedUpdate()
    {
        if (useFrame) { return; }
        if (Input.GetButtonDown("Throw")  || throwDirection != Vector2.zero)
        {
            buttonHeld = true;
        }
        if (buttonHeld)
        {
            currChargeTime += Time.fixedDeltaTime;
        }
        if (!isReadyThrow)
        {
            currReloadTime += Time.fixedDeltaTime;
            if (currReloadTime >= reloadTime)
            {
                isReadyThrow = true;
            }
            return;
        }

        if (Input.GetButtonUp("Throw") || throwDirection == Vector2.zero)
        {
            buttonHeld = false;
            if (currChargeTime > strongChargeTime) { shootType = ShootType.Strong; }
            else if (currChargeTime > normalChargeTime) { shootType = ShootType.Normal; }
            else { shootType = ShootType.Weak; }
            Throw(shootType);
            currChargeTime = 0;
            isReadyThrow = false;
            currReloadTime = 0;
        }
    }

    void Throw(ShootType strength)
    {
        float ThrowSpeed = 0;

        GameObject newObject = Instantiate(throwObject, throwOriginPoint.transform.position, Quaternion.identity);
        switch (strength) {
            case (ShootType.Weak):
                ThrowSpeed = WeakThrowForce;
                break;
            case (ShootType.Normal):
                ThrowSpeed = NormalThrowForce;
                break;
            case (ShootType.Strong):
                ThrowSpeed = StrongThrowForce;
                break;
            default:
                break;
        }

        Debug.Log(ThrowSpeed);

        newObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * ThrowSpeed, ForceMode.Impulse);
    }


}