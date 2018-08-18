using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/MovingState")]
public class MovingState : State
{
    public override void handle_input(PlayerStateController controller)
    {
        controller.isGrounded = Physics2D.OverlapCircle(controller.feetPos.position, controller.checkRadius, controller.whatIsGround);
        controller.moveInput = Input.GetAxisRaw("Horizontal");
        controller.Flip();

        // Decision
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            controller.ChangeState(controller.jumpingState);
        }
    }

    public override void update(PlayerStateController controller)
    {
        controller.rb.velocity = new Vector2(controller.moveInput * controller.speed, controller.rb.velocity.y);
    }

    public override void onEnter(PlayerStateController controller)
    {
        
    }

    public override void onExit(PlayerStateController controller)
    {
        
    }
}
