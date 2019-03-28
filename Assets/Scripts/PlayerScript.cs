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

    [SerializeField]
    private bool _isFlyingForward = false;


    public float rotationSpeed = 3.0f;

    private void Start()
    {
        //sync framerate to refresh rate
        QualitySettings.vSyncCount = 1;

        _rb = GetComponent<Rigidbody>();

        ovrGrababble01 = Thruster01.GetComponent<OVRGrabbable>();
        ovrGrababble02 = Thruster02.GetComponent<OVRGrabbable>();
    }

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
            gameObject.transform.Rotate(0.0f, Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickHorizontal") * rotationSpeed, 0.0f);
        }

        if (ovrGrababble01.isGrabbed && ovrGrababble02.isGrabbed)
        {
            gameObject.transform.Rotate(0.0f, posYdifference * rotationSpeed, 0.0f);
        }
    }

    public void ChangeFlightType()
    {
        _isFlyingForward = !_isFlyingForward;
    }

    public bool GetFlightType()
    {
        return _isFlyingForward;
    }
}
