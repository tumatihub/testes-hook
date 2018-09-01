using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    public abstract void handle_input(PlayerStateController controller);

    public abstract void update(PlayerStateController controller);

    public virtual void onExit(PlayerStateController controller) { }

    public virtual void onEnter(PlayerStateController controller) { }
}
