using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneFailsafe : MonoBehaviour {

	public Transform phoneResetHeight;
	public Transform phoneVelocityZeroHeight;

	private Vector3 initialPosition;
	private Rigidbody body;

	// Use this for initialization
	void Start () {
		initialPosition = transform.position;
		body = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y < phoneResetHeight.position.y) {
			transform.position = initialPosition;
			body.velocity = Vector3.zero;
		}

		if(transform.position.y > phoneVelocityZeroHeight.position.y) {
			body.velocity = Vector3.zero;
		}
	}
}
