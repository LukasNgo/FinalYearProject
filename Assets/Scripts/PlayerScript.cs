using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    private Rigidbody _rb;
    private CharacterController _charController;

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

        //if (_charController.isGrounded)
        //{
        //    _rb.useGravity = false;
        //}
        //else
        //    _rb.useGravity = true;
    }
}
