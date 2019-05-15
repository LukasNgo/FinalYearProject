using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private PlayerScript _playerScript;
    public Transform UICanvas;
    public TextMeshProUGUI UIText;
    public Camera mainCamera;

    void Start () {
        //doesnt work anymore
        //WristUIAnimator = GetComponentInChildren<Animator>();

        _playerScript = playerObject.GetComponent<PlayerScript>();
    }
	
	void Update () {
        if (OVRInput.GetDown(UIButton))
        {
            //Debug.Log(UIButton + "button pressed");

            //doesnt work anymore
            //WristUIAnimator.SetTrigger("WristUITrigger");
        }

        if (OVRInput.GetDown(FlightButton) && _playerScript.GetEquipedStatus())
        {
            //Debug.Log(FlightButton + "button pressed");
            AudioManager.singleton.Play("ChangeFlight");
            Rocket1Animator.SetTrigger("RotateRocket");
            Rocket2Animator.SetTrigger("RotateRocket");
            Rocket3Animator.SetTrigger("RotateRocket");
            Rocket4Animator.SetTrigger("RotateRocket");
            _playerScript.ChangeFlightType();
        }

        int m_playerSpeed = (int)playerObject.GetComponent<Rigidbody>().velocity.magnitude;
        int m_altitude = (int)playerObject.transform.position.y;
        string flightModeString;
        if (playerObject.GetComponent<PlayerScript>().GetFlightType())
        {
            flightModeString = "forward";
        }
        else
        {
            flightModeString = "up";
        }
        UICanvas.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
        UIText.text = "speed: " + m_playerSpeed + "\n" + "altitude: " + m_altitude + "\n" + "flight type: " + flightModeString;
    }
}
