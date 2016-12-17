using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneFlight : MonoBehaviour {

	public float verticalScale = 30.0f;
	public float horizontalScale = 4.0f;

	private BoxCollider phoneCollider;

	public void StartFlight(GameObject phone) {
		var phoneBody = phone.GetComponent<Rigidbody> ();

		Vector3 vel = new Vector3();
		vel.x = UnityEngine.Random.Range(-horizontalScale, horizontalScale);
		vel.y = verticalScale;
		phoneBody.velocity = vel;


		phoneCollider = phone.GetComponent<BoxCollider> ();
		phoneCollider.enabled = false;

		StartCoroutine("ReactiveColliderLater");
	}

	public IEnumerator ReactiveColliderLater() {
		yield return new WaitForSeconds(0.3f);
		phoneCollider.enabled = true;
	}

	

}
