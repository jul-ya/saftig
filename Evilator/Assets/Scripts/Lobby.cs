﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Lobby : MonoBehaviour {

	public Transform playerParent;
	public Transform[] playerPrefabs;
	public Transform spawnPointsParent;

	private List<InputDevice> usedDevices = new List<InputDevice>();

	private Transform randomPlayerPrefab {
		get {
			return playerPrefabs[Random.Range(0, playerPrefabs.Length)];
		}
	}

	private Vector3 randomSpawnPoint {
		get {
			int childIdx = Random.Range(0, spawnPointsParent.childCount);
			return spawnPointsParent.GetChild(childIdx).transform.position;
		}
	}

	void AddPlayer(InputDevice dev) {
		var player = GameObject.Instantiate(randomPlayerPrefab);
		player.transform.parent = playerParent;
		player.transform.position = randomSpawnPoint;

		Controls controls = player.GetComponent<Controls> ();
		controls.dev = dev;
	}

	void RemovePlayer(InputDevice dev) {
		usedDevices.Remove(dev);
	}

	void Update () {
		var dev = InputManager.ActiveDevice;
		if(dev.CommandWasPressed) {
			if(usedDevices.Contains(dev)) {
				//RemovePlayer(dev);
			} else {
				AddPlayer(dev);
			}
		}
	}
}