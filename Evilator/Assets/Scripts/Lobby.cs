using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Lobby : MonoBehaviour {

	public bool debugMode = true;

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
		return debugMode || player2.GetComponent<Controls> ().dev != null;
	}

	void AddInputDev(InputDevice dev) {
		if(player1.GetComponent<Controls> ().dev == null) {
			player1.GetComponent<Controls> ().dev = dev;
			player1.GetComponent<Player> ().InputDevice = dev;

			player1.GetComponent<PlayerPhysics> ().PerformJump();
			player1.GetComponent<Animator> ().SetTrigger("jump");

			SendMessageUpwards("PlayerJoined", player1.gameObject);

		} else if(player1.GetComponent<Controls> ().dev != dev) {
			player2.GetComponent<Controls> ().dev = dev;
			player2.GetComponent<Player> ().InputDevice = dev;

			player2.GetComponent<PlayerPhysics> ().PerformJump();
			player2.GetComponent<Animator> ().SetTrigger("jump");

			SendMessageUpwards("PlayerJoined", player2.gameObject);
		}
	}

	void EnableInput() {
		print("enabled input");
		player1.GetComponent<Controls> ().enabled = true;
		player2.GetComponent<Controls> ().enabled = true;
	}

	void Update () {
		if(orch.gamePhase == GamePhase.Lobby) {
			var dev = InputManager.ActiveDevice;

			if(dev.GetControl(joinControl).WasPressed) {
				AddInputDev(dev);
			}

			if(debugMode && InputManager.ActiveDevice.GetControl(startGameControl).WasPressed) {
				var p2Controls = player2.GetComponent<Controls> ();

				if(p2Controls.dev == null) {
					p2Controls.dev = InputManager.Devices[InputManager.Devices.Count - 1];
					SendMessageUpwards("PlayerJoined", player2.gameObject);
				}

				EnableInput();
			}

			/*if(CanStart()) {
				if(player2.GetComponent<Player> ().InputDevice == null) {
					Destroy(player2.gameObject);
				}

				EnableInput();

				SendMessageUpwards("GameStarted");

				// Since the game already started, the lobby is not needed anymore
				Destroy(gameObject);
			}*/
		}
	}
}
