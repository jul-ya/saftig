using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrift : MonoBehaviour {

	public float forceIntensity = 1000.0f;

	public void Drift(Vector3 direction) {
		StartCoroutine("DriftPlayersApart", direction);
	}

	IEnumerator DriftPlayersApart(Vector3 direction) {
		Debug.Log("DRIFFFFFFFTTTTTTTTTT – Physics disabled");


		GetComponent<PlayerPhysics> ().enabled = false;

		yield return null;

		GetComponent<Rigidbody> ().velocity = (direction * forceIntensity);

		yield return new WaitForSeconds(0.8f);

		GetComponent<PlayerPhysics> ().enabled = true;

		Debug.Log("DRIFFFFFFFTTTTTTTTTT – Physics enabled");
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
