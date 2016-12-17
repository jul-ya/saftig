using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneHand : MonoBehaviour {

	public Transform hand;
	public Transform phonePrefab;

	private GameObject phony;

	public bool hasPhone {
		set {
			phony.SetActive(value);
			SendMessageUpwards("setActivePlayer", GetComponent<Player> ());
		}

		get {
			return phony.activeSelf;
		}
	}

	public Vector3 phonePosition {
		get {
			return phony.transform.position;
		}
	}

	// Use this for initialization
	void Start () {
		phony = GameObject.Instantiate(phonePrefab, hand).gameObject;
		phony.transform.localPosition = Vector3.zero;
		phony.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
