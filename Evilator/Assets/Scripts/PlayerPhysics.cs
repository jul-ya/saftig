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
		float scaleSgn = Mathf.Sign(transform.localScale.x);
		float directionSgn = Mathf.Sign(direction);

		if(direction != 0.0f && scaleSgn != directionSgn) {
			Vector3 scale = transform.localScale;
			scale.x = -scale.x;
			transform.localScale = scale;
		}
	}

	public void PerformJump() {
		Vector3 velocity = body.velocity;
		float height = transform.position.y;

		if(height < 1.6f && velocity.y < 0.0001f) {
			velocity.y = jumpStartSpeed;
			body.velocity = velocity;
		}
	}

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -transform.up, 0.5f);
    }

}
