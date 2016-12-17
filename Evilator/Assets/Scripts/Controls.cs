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

    private Animator animator;
    private Material testMaterial;

	// Use this for initialization
	void Start () {
		physics = GetComponent<Physics> ();
        animator = GetComponent<Animator>();

        testMaterial = GetComponent<SkinnedMeshRenderer>().material;
        testMaterial.color = Color.blue;

		player = GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		float moveDirection = dev.GetControl(moveControl).Value;
		physics.PerformMove(moveDirection);

        if (moveDirection != 0.0f)
        {
            testMaterial.color = Color.red;
            animator.SetTrigger("walk");

        }else
        {
            testMaterial.color = Color.blue;
            animator.SetTrigger("idle");
        }


		if(dev.GetControl(jumpControl).WasPressed) {
			physics.PerformJump();

            testMaterial.color = Color.green;
            animator.SetTrigger("jump");

		}

		if(dev.GetControl(blockControl).WasPressed) {
			player.Block();
		} else if(dev.GetControl(attackControl).WasPressed) {
			player.Attack();
		} 
	}
}
