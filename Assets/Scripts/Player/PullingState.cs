using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/PullingState")]
public class PullingState : State
{

    public override void handle_input(PlayerStateController controller)
    {

        if (Input.GetButtonDown("Fire2"))
        {
            controller.isRetracting = true;
        }

        // Render Hook
        controller.hookLine.SetPosition(0, controller.transform.position);
        controller.hookLine.SetPosition(1, controller.hook.transform.position);
        

        // Decision to RetractingState
        if (controller.isRetracting)
        {
            controller.ChangeState(controller.retractingState);
        }
    }

    public override void update(PlayerStateController controller)
    {
        controller.rb.isKinematic = true;
        controller.transform.Translate(
            (controller.hook.position - controller.transform.position).normalized * controller.speedHookPlayer * Time.fixedDeltaTime, 
            Space.World
        );
    }

    public override void onEnter(PlayerStateController controller)
    {
    }
}
