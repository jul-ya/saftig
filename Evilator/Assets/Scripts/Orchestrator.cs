﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orchestrator : MonoBehaviour {

    public GameObject elevatorSoundPrefab;
	private GamePhase phase = GamePhase.None;

	public GamePhase gamePhase {
		get {
			return phase;
		}
	}

	// Use this for initialization
	void Start () {
        Instantiate(elevatorSoundPrefab);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LobbyOpened() {
		// When lobby first appears
		phase = GamePhase.Lobby;
	}

	void GameStarted() {
        // Game started, lobby will be destroyed upon next frame
		Debug.Log("Starting gaaaame");
		phase = GamePhase.Play;
	}
}
