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
		SendMessageUpwards("LobbyOpened");
	}

	void AddPlayer(InputDevice dev) {
		usedDevices.Add(dev);

		var player = GameObject.Instantiate(randomPlayerPrefab);
		player.transform.parent = playerParent;
		player.transform.position = randomSpawnPoint;

		Controls controls = player.GetComponent<Controls> ();
		controls.dev = dev;

		float hue = (lastPlayerHue != -1.0f) ? (lastPlayerHue + 0.5f) : Random.Range(0.0f, 1.0f);
		hue = (hue > 1) ? (hue-1) : hue;

		Debug.Log("Hue: " + hue);

		PlayerColor colors = player.GetComponent<PlayerColor> ();
		colors.skinHsv = new Vector3(hue, playerSturation, playerBrightness);
		colors.shirtHsv = new Vector3(0.0f, 0.0f, 1.0f);
		print("Tie color: " + new Vector3(hue + 0.1f, 1.0f, 1.0f));
		colors.tieHsv = new Vector3(hue + 0.1f, 1.0f, 1.0f);
		colors.tieHsv = new Vector3(1f, 1.0f, 1.0f);
		colors.suitHsv = new Vector3(0.0f, 0.0f, 0.0f);



		lastPlayerHue = hue;

		//SendMessageUpwards("PlayerJoined", player);
	}

	void SetPlayerColors(GameObject player) {

	}

	void RemovePlayer(InputDevice dev) {
		usedDevices.Remove(dev);
	}

	bool CanStart() {
		return usedDevices.Count == 2 || usedDevices.Count == 1;
	}

	void Update () {
		var dev = InputManager.ActiveDevice;

		if(dev.GetControl(joinControl).WasPressed && usedDevices.Count < 2) {
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
