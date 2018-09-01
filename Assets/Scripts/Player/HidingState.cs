using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/HidingState")]
public class HidingState : State
{

    public override void handle_input(PlayerStateController controller)
    {
        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("Move");
            controller.ChangeState(controller.movingState);
        }
    }

    public override void update(PlayerStateController controller)
    {
    }

    public override void onEnter(PlayerStateController controller)
    {
        controller.animator.SetBool("hide", true);
        controller.rb.velocity = Vector2.zero;
    }

    public override void onExit(PlayerStateController controller)
    {
        controller.animator.SetBool("hide", false);
    }
}
