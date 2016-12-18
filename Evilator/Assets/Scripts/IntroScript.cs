using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using InControl;

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
    private PhoneSystem phoneSystem;

    [SerializeField]
    private GameObject phone;

    [SerializeField]
    private bool debugMode = false;

    [SerializeField]
    private GameObject sparks;


    private bool startPhaseComplete = false;

    private bool acceptMenuInput = false;




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


    void Update()
    {
        if (startPhaseComplete &&  phoneSystem.calling)
        {
            startPhaseComplete = false;
            StartCoroutine(EndSequence());
        }


        if (acceptMenuInput)
        {
            if (InputManager.ActiveDevice.Action1)
            {
                SceneManager.LoadScene(0);
            }

            if (InputManager.ActiveDevice.Action2)
            {
                Application.Quit();
            }
        }
    }

    private IEnumerator IntroSequencePart1()
    {
        if (!debugMode)
        {
            phone.GetComponentInChildren<MeshRenderer>().enabled = false;
       
            cameraFade.FadeOutIn(panels[0], null);

            yield return new WaitForSeconds(0.5f);

            
            AudioSource audio = SoundManager.SoundManagerInstance.Play(elevatorStartSounds, Vector3.zero, 6f, 1, false);
            audio.time = 8.0f;
            audio.Play();

            yield return new WaitForSeconds(4.5f);

            //fade to black to the 2nd 
            

            yield return new WaitForSeconds(0.5f);
            cameraFade.FadeOutIn(panels[1], panels[0]);
            yield return new WaitForSeconds(4.0f);
            elevatorAmbience.StartMusic();

            cameraFade.FadeOutIn(panels[2], panels[1]);
            Camera.main.GetComponent<ShakeShakeShake>().Shake();
            yield return new WaitForSeconds(5.0f);

            //fade out the 2nd
            cameraFade.FadeOutIn(null, panels[2]);
      
            originalPoition = elevatorRigidBody.transform.position;
        
            SendMessageUpwards("LobbyOpened");


            yield return new WaitForSeconds(6);
            //StartCoroutine(EndSequence());
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
        //shakeShakeShake.Shake();

        elevatorRigidBody.isKinematic = false;
        elevatorRigidBody.useGravity = true;

        yield return new WaitForSeconds(2.0f);

        elevatorRigidBody.isKinematic = true;
        elevatorRigidBody.useGravity = false;

        cameraFade.FadeOutIn(panels[3], null);

        yield return new WaitForSeconds(3.0f);

        elevatorRigidBody.transform.position = originalPoition;
        phone.GetComponentInChildren<MeshRenderer>().enabled = true;
        sparks.SetActive(true);

        cameraFade.FadeOutIn(panels[4], panels[3]);
      
        yield return new WaitForSeconds(3.0f);

        cameraFade.FadeOutIn(null, panels[4]);


        players[0].transform.position = playerPositions[0];
        players[1].transform.position = playerPositions[1];
        phone.transform.position = phonePosition;


        elevator.StartMovement();


        players[0].GetComponent<Controls>().enabled = true;
        players[1].GetComponent<Controls>().enabled = true;

        SendMessageUpwards("GameStarted");

        startPhaseComplete = true;

        //StartCoroutine(EndSequence());
      
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


        yield return new WaitForSeconds(3.0f);
        acceptMenuInput = true;


        cameraFade.FadeOutIn(panels[5], null);

    }

   
}
