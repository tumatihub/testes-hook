using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/JumpingState")]
public class JumpingState : State
{
    bool _isPressingJump;
    float _jumpTimeCounter;
    float _yv;
    bool _canJump = true;

    public override void handle_input(PlayerStateController controller)
    {
        controller.isGrounded = Physics2D.OverlapCircle(controller.feetPos.position, controller.checkRadius, controller.whatIsGround);
        controller.moveInput = Input.GetAxisRaw("Horizontal");
        controller.Flip();
        if (Input.GetKey(KeyCode.Space))
        {
            _isPressingJump = true;
        }
        else
        {
            _canJump = false;
            _isPressingJump = false;
        }

        if (_jumpTimeCounter > 0)
        {
            _jumpTimeCounter -= Time.deltaTime;
        }
        else
        {
            _canJump = false;
        }

        // Decision
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            controller.ChangeState(controller.jumpingState);
        }
    }

    public override void update(PlayerStateController controller)
    {
        if (_isPressingJump && _canJump)
        {
            _yv = controller.jumpForce;
        }
        else
        {
            _yv = controller.rb.velocity.y;
        }
        controller.rb.velocity = new Vector2(controller.moveInput * controller.speed, _yv);
    }

    public override void onEnter(PlayerStateController controller)
    {
        controller.rb.velocity = Vector2.up * controller.jumpForce;
        _jumpTimeCounter = controller.jumpTime;
    }

    public override void onExit(PlayerStateController controller)
    {

    }
}
