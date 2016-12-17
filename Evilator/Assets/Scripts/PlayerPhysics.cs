using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour {

	public float jumpStartSpeed = 2.0f;
	public float walkSpeed = 1.0f;

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

		EnsureCorrectOrientation(direction);
	}

	public void EnsureCorrectOrientation(float direction) {
		if(Mathf.Abs(direction) > 0.1f) {
			float directionSgn = Mathf.Sign(direction);
			var col = GetComponent<BoxCollider> ();
			Vector3 colCenter = col.center;

			if(directionSgn != Mathf.Sign(colCenter.x)) {
				colCenter.x = -colCenter.x;
				col.center = colCenter;
			}
		}
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
