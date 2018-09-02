using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/FallingState")]
public class FallingState : State
{
    private float _relSpeed;
    private float _lastSpeed;

    public override void handle_input(PlayerStateController controller)
    {
        controller.moveInput = Input.GetAxisRaw("Horizontal");
        controller.Flip();

        if (!controller.isRetracted)
        {

            // Render Hook
            if (controller.hookLine.enabled)
            {
                controller.hookLine.SetPosition(0, controller.transform.position);
                controller.hookLine.SetPosition(1, controller.hook.transform.position);
            }
        }
        else
        {
            controller.hookLine.enabled = false;
            controller.hook.parent = controller.transform;
            controller.isRetracting = false;
            controller.hook.position = controller.transform.position;
        }

        // Decision to MovingState
        if (controller.isGrounded)
        {
            controller.ChangeState(controller.movingState);
        }
        
        

    }

    public override void update(PlayerStateController controller)
    {
        _lastSpeed += controller.moveInput * controller.fallingSpeed * Time.fixedDeltaTime;
        // Move player
        controller.rb.velocity = new Vector2(_lastSpeed, controller.rb.velocity.y);
        controller.isGrounded = Physics2D.OverlapCircle(controller.feetPos.position, controller.checkRadius, controller.whatIsGround);
        

        // Retract Hook
        if (!controller.isRetracted)
        {
            controller.hook.Translate(
                (controller.transform.position - controller.hook.transform.position).normalized * controller.speedHook * Time.fixedDeltaTime,
                Space.World
            );
        }
        
    }

    public override void onEnter(PlayerStateController controller)
    {
        _relSpeed = controller.rb.velocity.x;
        _lastSpeed = _relSpeed;
    }

    public override void onExit(PlayerStateController controller)
    {
        controller.hookLine.enabled = false;
        controller.hook.parent = controller.transform;
        controller.isRetracting = false;
        controller.hook.position = controller.transform.position;
    }
}
