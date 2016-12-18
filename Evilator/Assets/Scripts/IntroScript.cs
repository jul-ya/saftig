using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour {

    [SerializeField]
    private GameObject[] panels;

    [SerializeField]
    private AudioClip[] ropeCuts;

    [SerializeField]
    private AudioClip elevatorStartSounds;

    [SerializeField]
    private AudioClip elevatorEndSounds;

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
	

    private IEnumerator IntroSequencePart1()
    {
        if (!debugMode)
        {
            phone.GetComponent<MeshRenderer>().enabled = false;
            cameraFade.FadeOutIn(panels[0], null);

            yield return new WaitForSeconds(0.5f);

            
            AudioSource audio = SoundManager.SoundManagerInstance.Play(elevatorStartSounds, Vector3.zero, 6f, 1, false);
            audio.time = 11.0f;
            audio.Play();

            yield return new WaitForSeconds(4.5f);

            //fade to black to the 2nd 
            elevatorAmbience.StartMusic();

            yield return new WaitForSeconds(0.5f);
            cameraFade.FadeOutIn(panels[1], panels[0]);
            yield return new WaitForSeconds(5.0f);

            


            //fade out the 2nd
            cameraFade.FadeOutIn(null, panels[1]);
      
            originalPoition = elevatorRigidBody.transform.position;
        
            SendMessageUpwards("LobbyOpened");


            yield return new WaitForSeconds(6);
            StartCoroutine(EndSequence());
        }
        else
        {
            SendMessageUpwards("LobbyOpened");
            cameraFade.SetTransparent();
            elevator.StartMovement();
            elevatorAmbience.StartElevatorAmbience();
            yield return null;
        }
    }
    
    
    private IEnumerator IntroSequencePart2() {

        elevatorAmbience.StartElevatorAmbience();
        //yield return new WaitForSeconds(0.0f);
        shakeShakeShake.Shake();

        elevatorRigidBody.isKinematic = false;
        elevatorRigidBody.useGravity = true;

        yield return new WaitForSeconds(2.0f);

        elevatorRigidBody.isKinematic = true;
        elevatorRigidBody.useGravity = false;

        cameraFade.FadeOutIn(panels[2], null);

        yield return new WaitForSeconds(3.0f);

        elevatorRigidBody.transform.position = originalPoition;
        phone.GetComponent<MeshRenderer>().enabled = true;

        cameraFade.FadeOutIn(panels[3], panels[2]);
      
        yield return new WaitForSeconds(3.0f);

        cameraFade.FadeOutIn(null, panels[3]);


        players[0].transform.position = playerPositions[0];
        players[1].transform.position = playerPositions[1];
        phone.transform.position = phonePosition;


        elevator.StartMovement();


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

            SoundManager.SoundManagerInstance.Play(ropeCuts[Random.Range(0,ropeCuts.Length-1)], Vector3.zero, 6f, 1, false);

            if (playerCount > 1)
            {
                 StartCoroutine(IntroSequencePart2());
            }
        }
    }





    private IEnumerator EndSequence()
    {
        cameraFade.FadeOut();

        if (players[0] != null)
        {
            players[0].GetComponent<Controls>().enabled = false;
        }

        if (players[1] != null)
        {
            players[1].GetComponent<Controls>().enabled = false;
        }

        AudioSource audio = SoundManager.SoundManagerInstance.Play(elevatorEndSounds, Vector3.zero, 8f, 1, false);
        yield return new WaitForSeconds(7.0f);
        elevatorAmbience.DisableAllSounds();



    }

   
}
