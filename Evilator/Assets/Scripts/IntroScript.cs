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


    private Vector3 originalPoition;


    private bool playersCanJoin = false;

    // Use this for initialization
    void Start () {
        

        StartCoroutine(IntroSequence());
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    private IEnumerator IntroSequence()
    {
        if (!debugMode)
        {
            panels[0].SetActive(true);

            yield return new WaitForSeconds(3.0f);
            panels[0].SetActive(false);

            panels[1].SetActive(true);

            yield return new WaitForSeconds(3.0f);
            panels[1].SetActive(false);

            originalPoition = elevatorRigidBody.transform.position;

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
        }else
        {
            elevator.StartMovement();
            elevatorAmbience.StartElevatorAmbience();
            yield return null;


        }
        




    }
}
