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

	private Orchestrator orch;
	private Transform player1;
	private Transform player2;

	public float playerSturation = 0.9f;
	public float playerBrightness = 1.0f;

	private float lastPlayerHue = -1.0f;

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
		orch = GameObject.Find("Game").GetComponent<Orchestrator> ();
		AddPlayer(null);
		AddPlayer(null);
	}

	void AddPlayer(InputDevice dev) {
		usedDevices.Add(dev);

		var player = GameObject.Instantiate(randomPlayerPrefab);
		player.transform.parent = playerParent;
		player.transform.position = spawnPointsParent.GetChild((player1 == null) ? 0 : 1).transform.position;

		Controls controls = player.GetComponent<Controls> ();
		controls.enabled = false;
		controls.dev = dev;

		float hue = (lastPlayerHue != -1.0f) ? (lastPlayerHue + 0.5f) : Random.Range(0.0f, 1.0f);
		hue = (hue > 1) ? (hue-1) : hue;

		Debug.Log("Hue: " + hue);

		PlayerColor colors = player.GetComponent<PlayerColor> ();
		colors.skinHsv = new Vector3(hue, playerSturation, playerBrightness);
		colors.shirtHsv = new Vector3(0.0f, 0.0f, 1.0f);
		colors.tieHsv = new Vector3(hue + 0.1f, 1.0f, 1.0f);
		colors.tieHsv = new Vector3(1f, 1.0f, 1.0f);
		colors.suitHsv = new Vector3(0.0f, 0.0f, 0.0f);

		lastPlayerHue = hue;

		if(player1 == null) {
			player1 = player;
		} else {
			player2 = player;
		}

		//SendMessageUpwards("PlayerJoined", player);
	}

	void SetPlayerColors(GameObject player) {

	}

	void RemovePlayer(InputDevice dev) {
		usedDevices.Remove(dev);
	}

	bool CanStart() {
		return player2.GetComponent<Controls> ().dev != null;
	}

	void AddInputDev(InputDevice dev) {
		if(player1.GetComponent<Controls> ().dev == null) {
			player1.GetComponent<Controls> ().dev = dev;
		} else {
			player2.GetComponent<Controls> ().dev = dev;
		}

		print("Joined");
	}

	void EnableInput() {
		player1.GetComponent<Controls> ().enabled = true;
		player2.GetComponent<Controls> ().enabled = true;
	}

	void Update () {
		if(orch.gamePhase == GamePhase.Lobby) {
			var dev = InputManager.ActiveDevice;

			if(dev.GetControl(joinControl).WasPressed) {
				if(usedDevices.Contains(dev)) {
					//RemovePlayer(dev);
				} else {
					AddInputDev(dev);
				}
			}

			if(CanStart() && dev.GetControl(startGameControl).WasPressed) {
				EnableInput();

				SendMessageUpwards("GameStarted");

				// Since the game already started, the lobby is not needed anymore
				Destroy(gameObject);
			}
		}
	}
}
