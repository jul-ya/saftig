using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour {

    [SerializeField]
    private GameObject[] panels;

    [SerializeField]
    private Elevator elevator;

    [SerializeField]
    private ElevatorAmbience elevatorAmbience;

    [SerializeField]
    private Rigidbody elevatorRigidBody;

    [SerializeField]
    private ShakeShakeShake shakeShakeShake;


    [SerializeField]
    private bool debugMode = false;



    private CameraFade cameraFade;

    private Vector3 originalPoition;


    private bool playersCanJoin = false;


    private int connectedPlayers = 0;

    // Use this for initialization
    void Start () {

        cameraFade = GetComponent<CameraFade>();
        

        StartCoroutine(IntroSequencePart1());
	}
	
	// Update is called once per frame
	void Update () {

        if (playersCanJoin)
        {

            
            

        }		
	}



    private IEnumerator IntroSequencePart1()
    {
        cameraFade.FadeOutIn(panels[0], null);

        if (!debugMode)
        {
            yield return new WaitForSeconds(3.0f);

            //fade to black to the 2nd 
            cameraFade.FadeOutIn(panels[1], panels[0]);

            yield return new WaitForSeconds(3.0f);

            //fade out the 2nd
            cameraFade.FadeOutIn(null, panels[1]);
      
            originalPoition = elevatorRigidBody.transform.position;

            playersCanJoin = true;

            SendMessageUpwards("LobbyOpened");

            StartCoroutine(IntroSequencePart2());
        }else
        {
            elevator.StartMovement();
            elevatorAmbience.StartElevatorAmbience();
            yield return null;
        }
    }
    
    
    private IEnumerator IntroSequencePart2() {

        yield return new WaitForSeconds(4.0f);
        shakeShakeShake.Shake();



        elevatorRigidBody.isKinematic = false;
        elevatorRigidBody.useGravity = true;


        yield return new WaitForSeconds(2.0f);

        elevatorRigidBody.isKinematic = true;
        elevatorRigidBody.useGravity = false;

        

        cameraFade.FadeOutIn(panels[2], null);

        yield return new WaitForSeconds(3.0f);
        elevatorRigidBody.transform.position = originalPoition;

        cameraFade.FadeOutIn(panels[3], panels[2]);
      
        yield return new WaitForSeconds(3.0f);

        cameraFade.FadeOutIn(null, panels[3]);



        elevator.StartMovement();
        elevatorAmbience.StartElevatorAmbience();
        
    }
}
