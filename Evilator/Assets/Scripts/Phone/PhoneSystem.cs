using InControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 *  Script is responsible for the big phone in the interface.
 *  It handles all inputs of the player who has picked up the phone.
 *  The information of how much a player has already typed is saved in the Player script.
 *  
 *  SHOULD BE PLACED ON THE ACTUAL BIG PHONE MODEL
 **/
public class PhoneSystem : MonoBehaviour {

    /***
     * Joystick direction
     **/
    enum DIR {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        NONE
    }

    // Player who currently holds the phone
    private Player activePlayer;

    // Player who has been active before
    private Player lastActivePlayer;

    // current direction that has to be pressed
    private DIR activeDir;
    // pressed direction
    private DIR pressedDir;
    // how long current digit has been pressed (in seconds)
    float progressOfCurrentDigit;

    // how long does it take to enter a digit (in seconds)
    const float durationOfDigit = 0.3f;

    string typedDigits = "";

    bool calling = false;

    // UI STUFF
    public GameObject arrowUp;
    public GameObject arrowDown;
    public GameObject arrowLeft;
    public GameObject arrowRight;
    public GameObject innerCircle;
    public GameObject textfield;
    public GameObject callingmom;
    public GameObject circle;

    public Vector2 defaultPosInnerCircle;
    public AudioClip beep;

    private Dictionary<DIR, GameObject> arrowKeys = new Dictionary<DIR, GameObject>();

    public Vector2 defaultScale;
    
    private const float deadZone = 0.5f;

    // Use this for initialization
    void Start () {
        pressedDir = DIR.NONE;

        arrowKeys.Add(DIR.LEFT, arrowLeft);
        arrowKeys.Add(DIR.DOWN, arrowDown);
        arrowKeys.Add(DIR.UP, arrowUp);
        arrowKeys.Add(DIR.RIGHT, arrowRight);

        defaultPosInnerCircle = innerCircle.transform.position;

        defaultScale = arrowUp.transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        // check if already calling
        if (calling)
        {
            
        }
        //check if there is a active player with input device
        else if (activePlayer != null && activePlayer.InputDevice != null)
        {
            Vector2 steering = activePlayer.InputDevice.RightStick.Vector;
            innerCircle.transform.position = defaultPosInnerCircle + steering*13f;

            DIR pressedLast = pressedDir;

            // set direction
            if(activePlayer.InputDevice.RightStick.Left.Value > deadZone)
            {
                pressedDir = DIR.LEFT;
            }else if (activePlayer.InputDevice.RightStick.Right.Value > deadZone)
            {
                pressedDir = DIR.RIGHT;
            }else if (activePlayer.InputDevice.RightStick.Up.Value > deadZone)
            {
                pressedDir = DIR.UP;
            }else if (activePlayer.InputDevice.RightStick.Down.Value > deadZone)
            {
                pressedDir = DIR.DOWN;
            }else
            {
                pressedDir = DIR.NONE;
            }

            // if pressed direction has changed to last pressed direction
            if(pressedLast != pressedDir)
            {
                // reset scale
                foreach(GameObject a in arrowKeys.Values) {
                   // a.transform.localScale = defaultScale;
                }

                // scale up current
                GameObject arrow;
                arrowKeys.TryGetValue(pressedDir, out arrow);
                if(arrow != null)
                {
                    //arrow.transform.localScale = new Vector2(defaultScale.x * 1.2f, defaultScale.y * 1.2f);

                    // start shake on active arrow
                    if (pressedDir == activeDir)
                    {
                        StartShake(arrow);
                    }
                }
            }
            
            if (pressedDir == activeDir)
            {
                progressOfCurrentDigit += Time.deltaTime;

                if (progressOfCurrentDigit >= durationOfDigit)
                {
                    Debug.Log("Entered a digit!");

                    float pitch = Random.Range(0.8f, 1.2f);
                    SoundManager.SoundManagerInstance.Play(beep, Vector2.zero, 1f, pitch);

                    if (typedDigits.Length >= activePlayer.mumsPhoneNumber.Length) // compare length of typed nr to mums nr
                    {
                        Debug.Log("You typed mums number! Call her!");
                        calling = true;
                        callSuccessful();
                        /*
                        foreach (GameObject a in arrowKeys.Values)
                        {
                            a.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
                        }
                        */
                    }
                    else
                    {
                        if(activePlayer.mumsPhoneNumber.Length >= activePlayer.nrOfDigitsTyped) // check this
                        {
                            typedDigits += activePlayer.mumsPhoneNumber[activePlayer.nrOfDigitsTyped];
                            textfield.GetComponent<Text>().text = typedDigits;
                            activePlayer.nrOfDigitsTyped++;
                        }
                        else
                        {
                            Debug.Log("this shouldnt happen!!!");
                            Debug.Log(activePlayer.mumsPhoneNumber.Length + " " + activePlayer.nrOfDigitsTyped);
                        }



                        progressOfCurrentDigit = 0;
                        setRandomActiveDir();
                    }
                }
            }else
            {
                // if pressed dir is not active dir
                progressOfCurrentDigit = 0;
            }
        }else
        {
            if (activePlayer != null && activePlayer.InputDevice == null) { 
                    Debug.LogError("no input device");
            }

        }
	}

    void callSuccessful()
    {
        circle.SetActive(false);
        callingmom.SetActive(true);
    }

    // to be called when phone is picked up
    void setActivePlayer(Player p)
    {
        if(p != activePlayer)
        {
            Debug.Log("new active player");
            if (lastActivePlayer != null && lastActivePlayer != activePlayer)
            {
                lastActivePlayer.nrOfDigitsTyped = 0; // hehehe resetting the typed digits of the other player
                typedDigits = ""; // This would be juicy when animated
            }
            innerCircle.GetComponent<Image>().color = p.GetComponent<PlayerColor>().getMaterialColor(); // set inner circle color to player color
            activePlayer = p;
            progressOfCurrentDigit = 0;
            setRandomActiveDir();
        }

        
    }

    // to be called when phone is dropped
    void deactivatePhone()
    {
        lastActivePlayer = activePlayer;
        activePlayer.nrOfDigitsTyped = 0;
        typedDigits = "";
        textfield.GetComponent<Text>().text = "";
        activePlayer = null;
        innerCircle.GetComponent<Image>().color = new Color32(100, 100, 100, 255);
        Debug.Log("PHONE DROPPED");
    }

    void setRandomActiveDir()
    {
        Debug.Log("new dir");

        
        DIR newDir = activeDir;
        while(newDir == activeDir)
        {
            float rand = Random.Range(0, 4);
            if (rand < 1)
            {
                newDir = DIR.DOWN;
            }
            else if (rand < 2)
            {
                newDir = DIR.UP;
            }
            else if (rand < 3)
            {
                newDir = DIR.LEFT;
            }
            else
            {
                newDir = DIR.RIGHT;
            }
        }

        activeDir = newDir;

        foreach (GameObject a in arrowKeys.Values)
        {
            a.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
        }

        // color active arrow
        GameObject arrow;
        arrowKeys.TryGetValue(activeDir, out arrow);
        if (arrow != null)
        {
            arrow.GetComponent<Image>().color = new Color32(50, 240, 50, 255);
        }else
        {
            Debug.Log("Fail.");
        }
    }

    [SerializeField]
    private bool shake = false;

    [SerializeField]
    private float intensity = 10.0f;

    [SerializeField]
    private float shakeTime = 1.0f;

    private float currentShake = 10.0f;

    public void StartShake(GameObject g)
    {
        float defaultRotation = g.transform.rotation.eulerAngles.z;
        LeanTween.value(g, 0, 10f, durationOfDigit)
          .setOnUpdate((float amount) =>
          {
              currentShake = amount * intensity;
              g.transform.rotation = Quaternion.Euler(0,0,Random.Range(-currentShake, currentShake) * 0.2f + defaultRotation);
          })
          .setOnComplete(() =>
          {
              g.transform.rotation = Quaternion.Euler(0, 0, defaultRotation);
          })
          .setEase(LeanTweenType.easeOutSine);
    }
    
}
