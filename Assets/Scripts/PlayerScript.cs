using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private Rigidbody _rb;
    public GameObject Thruster01;
    public GameObject Thruster02;
    public GameObject LeftHand;
    public GameObject RightHand;
    private float posYdifference;
    private OVRGrabbable ovrGrababble01;
    private OVRGrabbable ovrGrababble02;
    public float rotationSpeed = 3.0f;
    private bool _isEquiped = false;
    [SerializeField]
    private bool _isFlyingForward = false;
    
    private void Start()
    {
        //sync framerate to refresh rate
        QualitySettings.vSyncCount = 1;

        _rb = GetComponent<Rigidbody>();

        ovrGrababble01 = Thruster01.GetComponent<OVRGrabbable>();
        ovrGrababble02 = Thruster02.GetComponent<OVRGrabbable>();
    }

    //calculate difference between left and right controller on Y axis
    private void Update()
    {
        posYdifference = LeftHand.transform.position.y - RightHand.transform.position.y;
    }

    private void FixedUpdate()
    {
        //prevent falling through the ground
        if (_rb.transform.position.y < 0f)
        {
            _rb.MovePosition(new Vector3(_rb.transform.position.x, 0f, _rb.transform.position.z));
        }

        //player rotation using right thumbstick
        if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickHorizontal") != 0)
        {
            gameObject.transform.Rotate(0.0f, Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickHorizontal") * 2, 0.0f);
        }

        //player rotation using hand position
        if (ovrGrababble01.isGrabbed && ovrGrababble02.isGrabbed)
        {
            gameObject.transform.Rotate(0.0f, posYdifference * rotationSpeed, 0.0f);
        }

        //player movement using thumbstick
        if (_rb.velocity.magnitude < 2)
        {
            if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") != 0)
            {
                //_rb.AddForce(transform.forward * Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical")/10, ForceMode.Impulse);
                _rb.velocity = transform.forward * Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") * 2;
            }
        }


        if (!GetEquipedStatus())
        {
            _rb.useGravity = true;
        }

        if (GetFlightType() && GetEquipedStatus())
        {
            _rb.useGravity = false;
        }
    }

    //change flight type between forward and up, also disable gravity if flying forward.
    public void ChangeFlightType()
    {
        if (ovrGrababble01.isGrabbed || ovrGrababble02.isGrabbed)
        {
            _isFlyingForward = !_isFlyingForward;

            if (_isFlyingForward)
            {
                _rb.useGravity = false;
            }

            if (!_isFlyingForward)
            {
                _rb.useGravity = true;
            }
        }
        else
        {
            _rb.useGravity = true;
        }
    }
    
    public bool GetEquipedStatus()
    {
        if (ovrGrababble01.isGrabbed || ovrGrababble02.isGrabbed)
        {
            _isEquiped = true;
        }
        else
        {
            _isEquiped = false;
        }

        return _isEquiped;
    }

    public bool GetFlightType()
    {
        return _isFlyingForward;
    }
}
