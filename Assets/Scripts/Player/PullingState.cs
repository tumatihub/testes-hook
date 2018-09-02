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
        

        // Decision to FallingState

        if (controller.isRetracting)
        {
            controller.rb.isKinematic = false;
            // Push to hook
            controller.rb.AddForce(
                (controller.hook.position - controller.transform.position).normalized * controller.hookInertia * Time.deltaTime, 
                ForceMode2D.Impulse
            );
            controller.ChangeState(controller.fallingState);
        }

        if (controller.isRetracted)
        {
            controller.rb.isKinematic = false;
            controller.hookLine.enabled = false;
            controller.hook.parent = controller.transform;
            controller.isRetracting = false;
            controller.hook.position = controller.transform.position;
            controller.ChangeState(controller.fallingState);
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
