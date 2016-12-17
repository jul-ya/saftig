using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics : MonoBehaviour {

	public float walkSpeed = 1.0f;

	private Vector3 veloctiy;

	/**
	 *  Called every frame with direction between -1 and 1, standing still is exactly 0.0.
	 */
	public void PerformMove(float direction) {
		Vector3 pos = transform.position;

		pos.x += direction * walkSpeed * Time.deltaTime;

		transform.position =  pos;
	}

	public void PerformJump() {

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
