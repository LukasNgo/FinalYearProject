using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGun : MonoBehaviour {

    private SimpleShoot simpleShoot;
    private OVRGrabbable ovrGrababble;
    public OVRInput.Button shootingButton;

	// Use this for initialization
	void Start () {
        simpleShoot = GetComponent<SimpleShoot>();
        ovrGrababble = GetComponent<OVRGrabbable>();
	}
	
	// Update is called once per frame
	void Update () {
        if (ovrGrababble.isGrabbed && OVRInput.GetDown(shootingButton, ovrGrababble.grabbedBy.GetController()))
        {
            simpleShoot.TriggerShoot();
        }
	}
}
