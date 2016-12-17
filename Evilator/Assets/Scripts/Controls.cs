﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Controls : MonoBehaviour {

	public InputControlType moveControl = InputControlType.LeftStickX;
	public InputControlType jumpControl = InputControlType.Action1;
	public InputControlType attackControl = InputControlType.Action3;
	public InputControlType blockControl = InputControlType.Action4;

	public InputDevice dev;

	private PlayerPhysics physics;
	private Player player;

    private Animator animator;

	// Use this for initialization
	void Start () {
		physics = GetComponent<PlayerPhysics> ();
        animator = GetComponent<Animator>();

		player = GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
        if (player.machine.CurrentStateID != StateID.StunState)
        {
            float moveDirection = dev.GetControl(moveControl).Value;
            physics.PerformMove(moveDirection);

            if (physics.IsGrounded())
            {
                if (moveDirection != 0.0f)
                {
                    animator.SetTrigger("walk");

                }
                else
                {
                    animator.SetTrigger("idle");
                }
            }


            if (dev.GetControl(jumpControl).WasPressed)
            {
                physics.PerformJump();
                
                animator.SetTrigger("jump");

            }

            if (dev.GetControl(blockControl).WasPressed)
            {
                player.Block();
            }
            else if (dev.GetControl(attackControl).WasPressed)
            {
                player.Attack();
            }
        } else
        {
            animator.SetTrigger("stun");
        }
	}
}
