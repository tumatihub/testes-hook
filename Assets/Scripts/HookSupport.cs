using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookSupport : MonoBehaviour, IHookable {

	public void HookAction() { }

    public string GetTag()
    {
        return gameObject.tag;
    }
}
