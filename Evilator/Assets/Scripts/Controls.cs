using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Controls : MonoBehaviour {

	public InputControlType moveControl = InputControlType.LeftStickX;
	public InputControlType jumpControl = InputControlType.Action1;

	public InputDevice dev;

	private Physics physics;

	// Use this for initialization
	void Start () {
		physics = GetComponent<Physics> ();
	}
	
	// Update is called once per frame
	void Update () {
		float moveDirection = dev.GetControl(moveControl).Value;
		physics.PerformMove(moveDirection);

		if(dev.GetControl(jumpControl).WasPressed) {
			physics.PerformJump();
		}
	}
}
