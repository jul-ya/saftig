using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Lobby : MonoBehaviour {

	public InputControlType joinControl = InputControlType.Action1;
	public InputControlType startGameControl = InputControlType.Start;

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

	void Start() {
		SendMessageUpwards("LobbyOpened");
	}

	void AddPlayer(InputDevice dev) {
		usedDevices.Add(dev);

		var player = GameObject.Instantiate(randomPlayerPrefab);
		player.transform.parent = playerParent;
		player.transform.position = randomSpawnPoint;

		Controls controls = player.GetComponent<Controls> ();
		controls.dev = dev;

		//SendMessageUpwards("PlayerJoined", player);
	}

	void RemovePlayer(InputDevice dev) {
		usedDevices.Remove(dev);
	}

	bool CanStart() {
		return usedDevices.Count > 0;
	}

	void Update () {
		var dev = InputManager.ActiveDevice;

		if(dev.GetControl(joinControl).WasPressed) {
			if(usedDevices.Contains(dev)) {
				//RemovePlayer(dev);
			} else {
				AddPlayer(dev);
			}
		}

		if(CanStart() && dev.GetControl(startGameControl).WasPressed) {
			SendMessageUpwards("GameStarted");

			// Since the game already started, the lobby is not needed anymore
			Destroy(gameObject);
		}
	}
}
