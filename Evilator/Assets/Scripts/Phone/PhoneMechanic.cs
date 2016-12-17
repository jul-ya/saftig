using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Script is responsible for the big phone in the interface
 *  and handles all inputs of player who has picked up the phone
 *  the information of how much a player has already typed in is saved in the Player script itself
 **/
public class PhoneMechanic : MonoBehaviour {

    /**
     * Joystick direction
     **/
    enum DIR {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    // Player who currently holds the phone
    Player activePlayer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // to be called when phone is picked up
    void setActivePlayer(Player p)
    {
        activePlayer = p;
    }

    void Press(DIR dir)
    {

    }

    void Release(DIR dir)
    {

    }
}
