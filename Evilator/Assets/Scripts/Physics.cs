using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour {

	public float jumpStartSpeed = 2.0f;
	public float walkSpeed = 1.0f;
	public float gravity = 9.81f;

	private Rigidbody body;

	void Start() {
		body = GetComponent<Rigidbody> ();
	}

	/**
	 *  Called every frame with direction between -1 and 1, standing still is exactly 0.0.
	 */
	public void PerformMove(float direction) {
		Vector3 velocity = body.velocity;
		velocity.x = direction * walkSpeed * Time.deltaTime;
		body.velocity = velocity;
	}

	public void PerformJump() {
		Vector3 velocity = body.velocity;
		velocity.y = jumpStartSpeed;
		body.velocity = velocity;
	}

	void ApplyGravity() {

	}

	void Move() {
		
	}
	
	// Update is called once per frame
	void Update () {
		ApplyGravity();
		Move();
	}
}
