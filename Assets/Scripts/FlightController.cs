using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightController : MonoBehaviour {

    public Rigidbody _rigidbody;
    public OVRInput.Button flightButton;
    private OVRGrabbable ovrGrababble;
    private float speed = 0.1f;
    public OVRPlayerController _ovrPlayerController;
    private ParticleSystem _particleFX;
    private ParticleSystem.EmissionModule _emmisionModule;
    public float maxSpeed = 10f;
    public float particleEmmisionRate = 75f;

    private void Start () {
        ovrGrababble = GetComponent<OVRGrabbable>();
        _particleFX = GetComponentInChildren<ParticleSystem>();
        _emmisionModule = _particleFX.emission;
    }

    private void FixedUpdate()
    {
        //limit maximum speed
        if (_rigidbody.velocity.magnitude > maxSpeed)
        {
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxSpeed);
        }

        //Primary Index Trigger button is recommended to use because it is more sensitive to touch 
        //and can adjust the speed by how much the button is pressed. Any other button can be used
        //as well but the player won't be able to control the speed
        if (flightButton == OVRInput.Button.PrimaryIndexTrigger)
        {
            if (ovrGrababble.isGrabbed && OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, ovrGrababble.grabbedBy.GetController()))
            {
                //0...1
                float speedMultiplier = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, ovrGrababble.grabbedBy.GetController());
                _rigidbody.AddForce(transform.up * speed * speedMultiplier, ForceMode.Impulse);
                GetComponent<Rigidbody>().AddForce(transform.up * speed * speedMultiplier, ForceMode.Impulse);

                _emmisionModule.rateOverTime = speedMultiplier * particleEmmisionRate;

                //if (speedMultiplier == 0)
                //{
                //    _ovrPlayerController.GravityModifier = 0.01f;
                //}
                //if (speedMultiplier > 0 && speedMultiplier < 0.5f)
                //{
                //    _ovrPlayerController.GravityModifier = 0.05f;
                //}
                //if (speedMultiplier > 0.5f)
                //{
                //    _ovrPlayerController.GravityModifier = 0;
                //}
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
                //_ovrPlayerController.GravityModifier = 0.0f;
                _rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
            }
            if (ovrGrababble.isGrabbed && OVRInput.GetUp(flightButton, ovrGrababble.grabbedBy.GetController()))
            {
                _rigidbody.velocity = _rigidbody.velocity * 0.99f;
                _rigidbody.angularVelocity = _rigidbody.angularVelocity * 0.99f;
            }
        }

        if (ovrGrababble.isGrabbed && OVRInput.GetDown(flightButton,ovrGrababble.grabbedBy.GetController()))
        {
            _particleFX.Play();
        }

        if (ovrGrababble.isGrabbed && OVRInput.GetUp(flightButton, ovrGrababble.grabbedBy.GetController()))
        {
            _particleFX.Stop();
        }

    }
}
