using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerState/ShootingState")]
public class ShootingState : State
{
    private Vector2 _whereToShoot;

    public override void handle_input(PlayerStateController controller)
    {
        if (Input.GetButtonDown("Fire2") || GetHookDistance(controller) >= controller.hookMaxDistance)
        {
            controller.isRetracting = true;
        }
        // Render Hook
        controller.hookLine.SetPosition(0, controller.transform.position);
        controller.hookLine.SetPosition(1, controller.hook.transform.position);
    }

    public override void update(PlayerStateController controller)
    {
        if (!controller.isHooked)
        {
            controller.hook.Translate(_whereToShoot * controller.speedHook * Time.fixedDeltaTime, Space.World);
        }

        // Decision to RetractingState
        if (controller.isRetracting)
        {
            controller.ChangeState(controller.retractingState);
        }

        // Decision to Hooked State
        if (controller.isHooked)
        {
            controller.ChangeState(controller.hookedState);
        }
    }

    public override void onEnter(PlayerStateController controller)
    {
        controller.rb.velocity = Vector2.zero;
        controller.hook.parent = null;
        controller.hookLine.enabled = true;
        _whereToShoot = controller.GetWhereToShoot();
    }

    float GetHookDistance(PlayerStateController controller)
    {
        return Vector2.Distance(controller.hook.position, controller.transform.position);
    }
}
