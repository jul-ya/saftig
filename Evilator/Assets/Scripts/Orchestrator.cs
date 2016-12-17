using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orchestrator : MonoBehaviour {

    public GameObject elevatorSoundPrefab;

	// Use this for initialization
	void Start () {
        Instantiate(elevatorSoundPrefab);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LobbyOpened() {
		// When lobby first appears
	}

	void GameStarted() {
        // Game started, lobby will be destroyed upon next frame
	}
}
