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
        // Render Hook
        controller.hookLine.SetPosition(0, controller.transform.position);
        controller.hookLine.SetPosition(1, controller.hook.transform.position);
    }

    public override void update(PlayerStateController controller)
    {
        // Decision
        if (controller.isRetracting)
        {
            Debug.Log("Go to retracting state");
        }
    }

    public override void onEnter(PlayerStateController controller)
    {
    }
}
