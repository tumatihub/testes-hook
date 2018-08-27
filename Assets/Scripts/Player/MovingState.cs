using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/MovingState")]
public class MovingState : State
{
    public override void handle_input(PlayerStateController controller)
    {
        controller.moveInput = Input.GetAxisRaw("Horizontal");
        controller.Flip();

    }

    public override void update(PlayerStateController controller)
    {
        controller.isGrounded = Physics2D.OverlapCircle(controller.feetPos.position, controller.checkRadius, controller.whatIsGround);
        controller.rb.velocity = new Vector2(controller.moveInput * controller.speed, controller.rb.velocity.y);

        // Decision to jump
        if (controller.isGrounded && Input.GetButton("Jump"))
        {
            Debug.Log("Going Jump");
            controller.ChangeState(controller.jumpingState);
        }

        // Decision to shoot
        if (controller.isGrounded && Input.GetButton("Fire1"))
        {
            Debug.Log("Going Shoot");
            controller.ChangeState(controller.shootingState);
        }
    }
}
