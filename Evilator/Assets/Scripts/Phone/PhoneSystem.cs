using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Script is responsible for the big phone in the interface.
 *  It handles all inputs of the player who has picked up the phone.
 *  The information of how much a player has already typed is saved in the Player script.
 *  
 *  SHOULD BE PLACED ON THE ACTUAL BIG PHONE MODEL
 **/
public class PhoneMechanic : MonoBehaviour {

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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(activePlayer != null)
        {
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
                    activeDir = getRandomDir();
                }
            }else
            {
                progressOfCurrentDigit = 0;
            }
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
    }

    // to be called when phone is dropped
    void deactivatePhone()
    {
        lastActivePlayer = activePlayer;
        activePlayer = null;
    }

    void Press(DIR dir)
    {
        pressedDir = dir;
    }

    void Release()
    {
        pressedDir = DIR.NONE;
    }
   

    DIR getRandomDir()
    {
        float rand = Random.Range(0, 4);
        if(rand < 1) {
            return DIR.DOWN;
        }else if(rand < 2) {
            return DIR.UP;
        }else if(rand < 3){
            return DIR.LEFT;
        } else {
            return DIR.RIGHT;
        } 
    }
}
