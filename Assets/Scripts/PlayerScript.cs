using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private Rigidbody _rb;
    private CharacterController _charController;

    public float rotationSpeed = 3.0f;

    private void Start()
    {
        //sync framerate to refresh rate
        QualitySettings.vSyncCount = 1;

        _rb = GetComponent<Rigidbody>();
        _charController = GetComponent<CharacterController>();
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

    }
}
