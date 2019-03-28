using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristUIController : MonoBehaviour {

    public OVRInput.Button UIButton;
    public OVRInput.Button FlightButton;
    public GameObject WristUIPrefab;
    public GameObject playerObject;
    public Animator WristUIAnimator;
    public Animator Rocket1Animator;
    public Animator Rocket2Animator;
    public Animator Rocket3Animator;
    public Animator Rocket4Animator;
    public Transform LeftHand;
    private bool _isLeft = false;
    private bool _isRight = false;
    private bool _isCentre = false;
    private PlayerScript _playerScript;

    void Start () {
        WristUIAnimator = GetComponentInChildren<Animator>();
        _playerScript = playerObject.GetComponent<PlayerScript>();
    }
	
	void Update () {
        if (OVRInput.GetDown(UIButton))
        {
            //Debug.Log(UIButton + "button pressed");
            WristUIAnimator.SetTrigger("WristUITrigger");
        }
        if (LeftHand.transform.localEulerAngles.z < 85 && LeftHand.transform.localEulerAngles.z > 60 && !_isRight)
        {
            _isLeft = false;
            _isCentre = false;
            WristUIAnimator.SetTrigger("WristUIRight");
            _isRight = true;
        }
        if (LeftHand.transform.localEulerAngles.z < 110 && LeftHand.transform.localEulerAngles.z > 85 && !_isCentre)
        {
            _isLeft = false;
            _isRight = true;
            WristUIAnimator.SetTrigger("WristUICentre");
            _isCentre = true;
        }
        if (LeftHand.transform.localEulerAngles.z < 135 && LeftHand.transform.localEulerAngles.z > 110 && !_isLeft)
        {
            _isRight = false;
            _isCentre = false;
            WristUIAnimator.SetTrigger("WristUILeft");
            _isLeft = true;
        }
        if (OVRInput.GetDown(FlightButton))
        {
            //Debug.Log(FlightButton + "button pressed");
            AudioManager.singleton.Play("ChangeFlight");
            Rocket1Animator.SetTrigger("RotateRocket");
            Rocket2Animator.SetTrigger("RotateRocket");
            Rocket3Animator.SetTrigger("RotateRocket");
            Rocket4Animator.SetTrigger("RotateRocket");
            _playerScript.ChangeFlightType();
        }
    }
}
