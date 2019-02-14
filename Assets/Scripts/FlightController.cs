using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController : MonoBehaviour {

    public Rigidbody _rigidbody;
    public OVRInput.Button flightButton;
    private OVRGrabbable ovrGrababble;
    private float speed = 5;

    // Use this for initialization
    private void Start () {
        ovrGrababble = GetComponent<OVRGrabbable>();
    }

    private void FixedUpdate()
    {
        if (ovrGrababble.isGrabbed && OVRInput.GetDown(flightButton, ovrGrababble.grabbedBy.GetController()))
        {
            _rigidbody.AddForce(transform.forward * speed, ForceMode.Acceleration);
        }
        if (ovrGrababble.isGrabbed && OVRInput.GetUp(flightButton, ovrGrababble.grabbedBy.GetController()))
        {
            _rigidbody.velocity = Vector3.zero;
        }

    }
}
