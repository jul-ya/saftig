using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Controls : MonoBehaviour {

	public InputControlType moveControl = InputControlType.LeftStickX;
	public InputControlType jumpControl = InputControlType.Action1;
	public InputControlType attackControl = InputControlType.Action3;
	public InputControlType blockControl = InputControlType.Action4;

	public InputDevice dev;

	private Physics physics;
	private Player player;

	// Use this for initialization
	void Start () {
		physics = GetComponent<Physics> ();
		player = GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		float moveDirection = dev.GetControl(moveControl).Value;
		physics.PerformMove(moveDirection);

		if(dev.GetControl(jumpControl).WasPressed) {
			physics.PerformJump();
		}

		if(dev.GetControl(blockControl).WasPressed) {
			player.Block();
		} else if(dev.GetControl(attackControl).WasPressed) {
			player.Attack();
		} 
	}
}
