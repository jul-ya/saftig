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
        if (!debugMode)
        {
            panels[0].SetActive(true);

            yield return new WaitForSeconds(3.0f);
            
            cameraFade.FadeOutIn();
            yield return new WaitForSeconds(0.2f);
            panels[0].SetActive(false);
            panels[1].SetActive(true);

            yield return new WaitForSeconds(3.0f);

            cameraFade.FadeOutIn();
            yield return new WaitForSeconds(0.2f);
           
            panels[1].SetActive(false);

            originalPoition = elevatorRigidBody.transform.position;

            playersCanJoin = true;
        }else
        {
            elevator.StartMovement();
            elevatorAmbience.StartElevatorAmbience();
            yield return null;
        }
    }
    
    
    private IEnumerator IntroSequencePart2() {  
        elevatorRigidBody.isKinematic = false;
        elevatorRigidBody.useGravity = true;


        yield return new WaitForSeconds(2.0f);

        elevatorRigidBody.isKinematic = true;
        elevatorRigidBody.useGravity = false;

        elevatorRigidBody.transform.position = originalPoition;
        panels[2].SetActive(true);

        yield return new WaitForSeconds(3.0f);
        panels[3].SetActive(true);

        panels[2].SetActive(false);

        yield return new WaitForSeconds(3.0f);

        panels[3].SetActive(false);



        elevator.StartMovement();
        elevatorAmbience.StartElevatorAmbience();
        
    }
}
