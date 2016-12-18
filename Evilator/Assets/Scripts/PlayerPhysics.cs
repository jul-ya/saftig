using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour {

	public float jumpStartSpeed = 2.0f;
	public float walkSpeed = 1.0f;

	private Rigidbody body;
	private Animator animator;
	private Vector3 lastPosition;

	void Start() {
		body = GetComponent<Rigidbody> ();
		animator = GetComponent<Animator> ();
		lastPosition = transform.position;
	}

	void Update() {
		EnsureCorrectOrientation();
	}

	/**
	 *  Called every frame with direction between -1 and 1, standing still is exactly 0.0.
	 */
	public void PerformMove(float direction) {
		Vector3 velocity = body.velocity;
		velocity.x = direction * walkSpeed * Time.deltaTime;
		body.velocity = velocity;
	}

	public void EnsureCorrectOrientation() {
		Vector3 newPosition = transform.position;

		float xDelta = newPosition.x - lastPosition.x;

		if(xDelta > 0.01f) {
			print("right: " + xDelta);
			SetFacingRight(true);
		} else if(xDelta < -0.01f) {
			print("left" + xDelta);
			SetFacingRight(false);
		}

		lastPosition = newPosition;
	}

	private void SetFacingRight(bool right) {
		/*var col = GetComponent<BoxCollider> ();
		Vector3 colCenter = col.center;
		colCenter.z = right ? Mathf.Abs(colCenter.z) : -Mathf.Abs(colCenter.z);
		col.center = colCenter;*/

		Quaternion rot = Quaternion.Euler(new Vector3(0.0f, right ? 0.0f : 180.0f, 0.0f));
		transform.localRotation = rot;
	}

	public void PerformJump() {
		Vector3 velocity = body.velocity;
		float height = transform.position.y;

		if(IsGrounded()) {
			velocity.y = jumpStartSpeed;
			body.velocity = velocity;
		}
	}

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -transform.up, 0.1f);
    }

}
