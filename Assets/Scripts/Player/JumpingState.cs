using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/JumpingState")]
public class JumpingState : State
{
    [SerializeField] bool _isPressingJump;
    [SerializeField] float _jumpTimeCounter;
    [SerializeField] float _yv;
    [SerializeField] bool _canJump = true;
    Collider2D _result;

    public override void handle_input(PlayerStateController controller)
    {
        if (controller.isGrounded)
        {
            Debug.Log("Grounded: " + controller.isGrounded);
        }
        else
        {
            Debug.Log("NOT Grounded");
        }
        controller.moveInput = Input.GetAxisRaw("Horizontal");
        controller.Flip();
        if (Input.GetButton("Jump"))
        {
            Debug.Log("Holding Jump.");
            _isPressingJump = true;
        }
        else
        {
            Debug.Log("Released Jump.");
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

        
    }

    public override void update(PlayerStateController controller)
    {
        Debug.Log("Jumping FixedUpdate.");
        if (_isPressingJump && _canJump)
        {
            _yv = controller.jumpForce;
        }
        else
        {
            if (controller.rb.velocity.y > 0)
            {
                _yv = controller.rb.velocity.y / 2;
            }
            else
            {
                _yv = controller.rb.velocity.y;
            }
            
        }
        controller.rb.velocity = new Vector2(controller.moveInput * controller.speed, _yv);
        //controller.isGrounded = Physics2D.OverlapCircle(controller.feetPos.position, controller.checkRadius, controller.whatIsGround);
        _result = Physics2D.OverlapCircle(controller.feetPos.transform.position, controller.checkRadius, controller.whatIsGround);
        if(_result != null)
        {
            Debug.Log(_result.gameObject.tag);
            Debug.DrawLine(controller.transform.position, _result.gameObject.transform.position);
            controller.isGrounded = true;
        }
        else
        {
            controller.isGrounded = false;
        }

        // Decision
        if (controller.isGrounded)
        {
            _isPressingJump = false;
            _canJump = true;
            controller.ChangeState(controller.movingState);
        }
    }

    public override void onEnter(PlayerStateController controller)
    {
        Debug.Log("Enter Jumping State.");
        controller.isGrounded = false;
        controller.rb.velocity = Vector2.up * controller.jumpForce;
        _isPressingJump = true;
        _jumpTimeCounter = controller.jumpTime;
    }
}
