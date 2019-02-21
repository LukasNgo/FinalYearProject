using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController : MonoBehaviour {

    public Rigidbody _rigidbody;
    public OVRInput.Button flightButton;
    private OVRGrabbable ovrGrababble;
    private float speed = 5;
    private Vector3 refVelocity = Vector3.zero;

    private void Start () {
        ovrGrababble = GetComponent<OVRGrabbable>();
    }

    private void FixedUpdate()
    {
        //Primary Index Trigger button is recommended to use because it is more sensitive to touch 
        //and can adjust the speed by how much the button is pressed. Any other button can be used
        //as well but the player won't be able to control the speed
        if (flightButton == OVRInput.Button.PrimaryIndexTrigger)
        {
            if (ovrGrababble.isGrabbed && OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, ovrGrababble.grabbedBy.GetController()))
            {
                float speedMultiplier = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, ovrGrababble.grabbedBy.GetController());
                _rigidbody.AddForce(transform.forward * speed * speedMultiplier, ForceMode.Acceleration);
            }
            if (ovrGrababble.isGrabbed && !OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, ovrGrababble.grabbedBy.GetController()))
            {
                _rigidbody.velocity = _rigidbody.velocity * 0.99f;
                _rigidbody.angularVelocity = _rigidbody.angularVelocity * 0.99f;

            }
        }
        else
        {
            if (ovrGrababble.isGrabbed && OVRInput.Get(flightButton, ovrGrababble.grabbedBy.GetController()))
            {
                _rigidbody.AddForce(transform.forward * speed, ForceMode.Acceleration);
            }
            if (ovrGrababble.isGrabbed && OVRInput.GetUp(flightButton, ovrGrababble.grabbedBy.GetController()))
            {
                _rigidbody.velocity = _rigidbody.velocity * 0.99f;
                _rigidbody.angularVelocity = _rigidbody.angularVelocity * 0.99f;
            }
        }


    }
}
