using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class Controls : MonoBehaviour {

	public InputControlType moveControl = InputControlType.LeftStickX;
	public InputControlType jumpControl = InputControlType.Action1;
	public InputControlType attackControl = InputControlType.Action3;
	public InputControlType blockControl = InputControlType.Action4;
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
	
	// Update is called once per frame
	void Update () {
        ResetTriggers();

        if (player.machine.CurrentStateID == StateID.StunState)
            animator.SetTrigger("stun");
        if (player.machine.CurrentStateID == StateID.CrouchState)
            animator.SetTrigger("crouch");

        if (orch.gamePhase == GamePhase.Play && player.machine.CurrentStateID != StateID.StunState)
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

			bool crouching = dev.GetControl(crouchAxis).Value < -0.9f;
			if (player.machine.CurrentStateID != StateID.CrouchState && crouching) {
				player.Crouch();
			} else if(player.machine.CurrentStateID == StateID.CrouchState && !crouching) {
				player.Uncrouch();
			}


        }
	}

    void ResetTriggers()
    {
        animator.ResetTrigger("walk");
        animator.ResetTrigger("idle");
        animator.ResetTrigger("stun");
        animator.ResetTrigger("jump");
        animator.ResetTrigger("crouch");

    }
}
