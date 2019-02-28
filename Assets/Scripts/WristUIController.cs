using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristUIController : MonoBehaviour {

    public OVRInput.Button UIButton;
    public GameObject WristUIPrefab;
    public Animator WristUIAnimator;
    public Transform LeftHand;
    private bool _isLeft = false;
    private bool _isRight = false;
    private bool _isCentre = false;

	// Use this for initialization
	void Start () {
        WristUIAnimator = GetComponentInChildren<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetDown(UIButton))
        {
            Debug.Log(UIButton + "button pressed");
            WristUIAnimator.SetTrigger("WristUITrigger");
        }
        if (LeftHand.transform.eulerAngles.z < 85 && LeftHand.transform.eulerAngles.z > 60 && !_isRight)
        {
            _isLeft = false;
            _isCentre = false;
            WristUIAnimator.SetTrigger("WristUIRight");
            _isRight = true;
        }
        if (LeftHand.transform.eulerAngles.z < 110 && LeftHand.transform.eulerAngles.z > 85 && !_isCentre)
        {
            _isLeft = false;
            _isRight = true;
            WristUIAnimator.SetTrigger("WristUICentre");
            _isCentre = true;
        }
        if (LeftHand.transform.eulerAngles.z < 135 && LeftHand.transform.eulerAngles.z > 110 && !_isLeft)
        {
            _isRight = false;
            _isCentre = false;
            WristUIAnimator.SetTrigger("WristUILeft");
            _isLeft = true;
        }
    }
}
