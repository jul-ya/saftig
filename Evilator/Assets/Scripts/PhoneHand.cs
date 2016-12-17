using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneHand : MonoBehaviour {

	public Transform hand;
	public Transform phonePrefab;

	private GameObject phone;

	public bool hasPhone {
		set {
			phone.SetActive(value);
			SendMessageUpwards("setActivePlayer", GetComponent<Player> ());
		}

		get {
			return phone.activeSelf;
		}
	}

	// Use this for initialization
	void Start () {
		phone = GameObject.Instantiate(phonePrefab, hand).gameObject;
		phone.transform.localPosition = Vector3.zero;
		phone.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
