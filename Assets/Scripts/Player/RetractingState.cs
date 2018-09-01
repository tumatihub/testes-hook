using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/RetractingState")]
public class RetractingState : State
{

    public override void handle_input(PlayerStateController controller)
    {
        // Render Hook
        controller.hookLine.SetPosition(0, controller.transform.position);
        controller.hookLine.SetPosition(1, controller.hook.transform.position);

        // Decision
        if (controller.isRetracted)
        {
            Debug.Log("Go to retracting state");
            controller.ChangeState(controller.movingState);
        }
    }

    public override void update(PlayerStateController controller)
    {
        controller.hook.Translate(
            (controller.transform.position - controller.hook.transform.position).normalized * controller.speedHook * Time.fixedDeltaTime, 
            Space.World
        );

    }

    public override void onExit(PlayerStateController controller)
    {
        controller.hookLine.enabled = false;
        controller.hook.parent = controller.transform;
        controller.isRetracting = false;
        controller.hook.position = controller.transform.position;
    }

    public override void onEnter(PlayerStateController controller)
    {
    }
}
