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
    const float durationOfDigit = 1f;

    string typedDigits = "";

    // UI STUFF
    public GameObject arrowUp;
    public GameObject arrowDown;
    public GameObject arrowLeft;
    public GameObject arrowRight;

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

        defaultScale = arrowUp.transform.localScale;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(InputManager.ActiveDevice.RightStick.Left.Value);
        if (activePlayer != null)
        {
            DIR pressedLast = pressedDir;

            // calc direction
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

            // if direction has changed
            if(pressedLast != pressedDir)
            {
                // Debug.Log("pressed " + pressedDir.ToString());
                arrowRight.transform.localScale = new Vector2(1, 1);
                arrowLeft.transform.localScale = new Vector2(1,1);
                arrowUp.transform.localScale = new Vector2(1, 1);
                arrowDown.transform.localScale = new Vector2(1, 1);

                GameObject arrow;
                arrowKeys.TryGetValue(pressedDir, out arrow);
                if(arrow != null)
                {
                    arrow.transform.localScale = new Vector2(1.2f, 1.2f);
                }
            }
            
            if (pressedDir == activeDir)
            {
                progressOfCurrentDigit += Time.deltaTime;

                if (progressOfCurrentDigit >= durationOfDigit)
                {
                    Debug.Log("Entered a digit!");
                    typedDigits += activePlayer.mumsPhoneNumber[activePlayer.nrOfDigitsTyped];
                    activePlayer.nrOfDigitsTyped++;

                    if (typedDigits.Length == activePlayer.mumsPhoneNumber.Length) // compare length of typed nr to mums nr
                    {
                        Debug.Log("You typed mums number! Call her!");
                    }

                    progressOfCurrentDigit = 0;
                    setRandomActiveDir();
                }
            }else
            {
                progressOfCurrentDigit = 0;
            }
        }else
        {
            // JUST FOR TESTING, DELETE THIS SOON
            activePlayer = FindObjectOfType<Player>();
            if(activePlayer != null)
            {
                Debug.Log("PLAYER FOUND");
            }
            //Debug.Log(activePlayer);
        }
	}

    // to be called when phone is picked up
    void setActivePlayer(Player p)
    {
        if(lastActivePlayer != null && lastActivePlayer != activePlayer)
        {
            lastActivePlayer.nrOfDigitsTyped = 0; // hehehe resetting the typed digits of the other player
        }
        activePlayer = p;
        typedDigits = ""; // This would be juicy when animated
        progressOfCurrentDigit = 0;
        setRandomActiveDir();
    }

    // to be called when phone is dropped
    void deactivatePhone()
    {
        lastActivePlayer = activePlayer;
        activePlayer = null;
    }

    void setRandomActiveDir()
    {
        float rand = Random.Range(0, 4);
        if(rand < 1) {
            activeDir = DIR.DOWN;
        }else if(rand < 2) {
            activeDir = DIR.UP;
        }else if(rand < 3){
            activeDir = DIR.LEFT;
        } else {
            activeDir = DIR.RIGHT;
        }

        switch (activeDir)
        {
            case DIR.LEFT:
                arrowLeft.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
                break;
            case DIR.RIGHT:
                arrowRight.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
                break;
            case DIR.UP:
                arrowUp.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
                break;
            case DIR.DOWN:
                arrowDown.GetComponent<Image>().color = new Color32(0, 255, 0, 255);
                break;
        }
    }
}
