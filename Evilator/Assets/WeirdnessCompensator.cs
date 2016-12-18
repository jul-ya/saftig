using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeirdnessCompensator : MonoBehaviour {

	public Transform reference;

	public string zReferenceTag = "Z";
	
	// Update is called once per frame
	void Update () {
		Vector3 offset = Vector3.zero - reference.transform.localPosition;
		offset.y = 0.0f;
		offset.z = -0.97f;
		offset.x = -0.0f;
		//offset.z = 0.0f;

		float referenceZ = GameObject.FindGameObjectWithTag(zReferenceTag).transform.position.z;

		transform.localPosition = offset;

		/*Vector3 worldPosition = transform.position;
		worldPosition.z = referenceZ;


		worldPosition += -transform.right * 2.25f;

		transform.position = worldPosition;*/
	}
}
