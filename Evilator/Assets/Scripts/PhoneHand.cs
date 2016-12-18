using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneHand : MonoBehaviour {

	public Vector3 localPositionInHand = new Vector3(0.239f, -0.074f, 0.065f);
	public Vector3 localRotationInHand = new Vector3(-36.795f, -90f, -66.369f);

    public AudioClip pickup;

	public Transform hand;
	public Transform phonePrefab;

	private GameObject phony;

	public bool hasPhone {
		set {
			phony.SetActive(value);
			SendMessageUpwards("setActivePlayer", GetComponent<Player> ());
            if (value)
                SoundManager.SoundManagerInstance.Play(pickup, Vector3.zero);
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
		phony.transform.localPosition = localPositionInHand;
		phony.transform.localRotation = Quaternion.Euler(localRotationInHand);
		phony.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
