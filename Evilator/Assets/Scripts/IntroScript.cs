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
    private GameObject phone;

    [SerializeField]
    private bool debugMode = false;


    private GameObject[] players = new GameObject[2];
    private Vector3[] playerPositions = new Vector3[2];

    private Vector3 phonePosition;

    private int playerCount = 0;
   
    private CameraFade cameraFade;

    private Vector3 originalPoition;

    private bool playersCanJoin = false;

    private int connectedPlayers = 0;

    // Use this for initialization
    void Start () {

        cameraFade = GetComponent<CameraFade>();
        phone = GameObject.FindGameObjectWithTag("Phone");
        phonePosition = phone.transform.position;

        StartCoroutine(IntroSequencePart1());
	}
	
	// Update is called once per frame
	void Update () {

       
	}

    private IEnumerator IntroSequencePart1()
    {
        if (!debugMode)
        {
            cameraFade.FadeOutIn(panels[0], null);

            yield return new WaitForSeconds(3.0f);

            //fade to black to the 2nd 
            cameraFade.FadeOutIn(panels[1], panels[0]);

            yield return new WaitForSeconds(3.0f);

            //fade out the 2nd
            cameraFade.FadeOutIn(null, panels[1]);
      
            originalPoition = elevatorRigidBody.transform.position;
        
            SendMessageUpwards("LobbyOpened");
        }else
        {
            SendMessageUpwards("LobbyOpened");
            cameraFade.SetTransparent();
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


        players[0].transform.position = playerPositions[0];
        players[1].transform.position = playerPositions[1];
        phone.transform.position = phonePosition;


        elevator.StartMovement();
        elevatorAmbience.StartElevatorAmbience();



        players[0].GetComponent<Controls>().enabled = true;
        players[1].GetComponent<Controls>().enabled = true;

        SendMessageUpwards("GameStarted");
    }

    public void PlayerJoined(GameObject player)
    {
        if (player != null)
        {
            players[playerCount] = player;
            playerPositions[playerCount] = players[playerCount].transform.position;
            playerCount++;

            if (playerCount > 1)
            {
                 StartCoroutine(IntroSequencePart2());
            }
        }
    }
}
