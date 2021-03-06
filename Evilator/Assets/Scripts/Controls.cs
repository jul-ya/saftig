﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Controls : MonoBehaviour {

	public InputControlType moveControl = InputControlType.LeftStickX;
	public InputControlType jumpControl = InputControlType.Action1;
	public InputControlType attackControl = InputControlType.Action3;
	public InputControlType blockControl = InputControlType.Action2;
	public InputControlType crouchAxis = InputControlType.LeftStickY;


	public InputDevice dev;

	private PlayerPhysics physics;
	private Player player;

    private Animator animator;

	private Orchestrator orch;

	// Use this for initialization
	void Start () {
		physics = GetComponent<PlayerPhysics> ();
        animator = GetComponent<Animator>();

		player = GetComponent<Player> ();

		orch = GameObject.Find("Game").GetComponent<Orchestrator> ();
	}

    void FixedUpdate()
    {
        if (player.machine.CurrentStateID == StateID.StunState)
        {
            player.stunP.Emit(1);
        }
    }

    // Update is called once per frame
    void Update() {
        if (orch.gamePhase == GamePhase.Play)
        {
            ResetTriggers();
			bool hasPhone = GetComponent<PhoneHand> ().hasPhone;

			if(dev.RightStick.Value.magnitude > 0.5f && hasPhone) {
				player.machine.PerformTransition(StateID.TypingState);
			}

            if (player.machine.CurrentStateID == StateID.StunState)
            {
                animator.SetTrigger("stun");
            }
            else if (player.machine.CurrentStateID != StateID.StunState)
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
                    //animator.SetTrigger("block");
                }
                else if (dev.GetControl(attackControl).WasPressed)
                {
                    player.Attack();
                    //animator.SetTrigger("attack");
                }
            }
            
            switch(player.machine.CurrentStateID){
                case StateID.BlockState:
                    animator.SetTrigger("block");
                    animator.SetLayerWeight(1, 1);
                    break;
                case StateID.AttackState:
                    animator.SetTrigger("attack");
                    animator.SetLayerWeight(1, 1);
                    break;
                case StateID.TypingState:
                    animator.SetTrigger("type");
                    animator.SetLayerWeight(1, 1);
                    break;
                default:
                    animator.SetLayerWeight(1, 0);
                    break;
            }
        }
    }

    void ResetTriggers()
    {
        animator.ResetTrigger("walk");
        animator.ResetTrigger("idle");
        animator.ResetTrigger("stun");
        animator.ResetTrigger("jump");
        animator.ResetTrigger("block");
        animator.ResetTrigger("attack");
        animator.ResetTrigger("type");
    }
}
