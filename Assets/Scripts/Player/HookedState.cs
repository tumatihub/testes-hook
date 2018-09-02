using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/HookedState")]
public class HookedState : State
{

    public override void handle_input(PlayerStateController controller)
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (controller.hookedObject != null)
            {
                controller.hookedObject.HookAction();
            }
            controller.isRetracting = true;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            controller.isRetracting = true;
        }

        // Render Hook
        controller.hookLine.SetPosition(0, controller.transform.position);
        controller.hookLine.SetPosition(1, controller.hook.transform.position);

        // Decision to PullingState
        if (controller.hookedObject != null)
        {
            if (controller.hookedObject.GetTag() == "HookSupport")
            {
                controller.ChangeState(controller.pullingState);
            }
        }
        

        // Decision to RetractingState
        if (controller.isRetracting)
        {
            controller.ChangeState(controller.retractingState);
        }
    }

    public override void update(PlayerStateController controller)
    {
        
    }

    public override void onEnter(PlayerStateController controller)
    {
    }
}
