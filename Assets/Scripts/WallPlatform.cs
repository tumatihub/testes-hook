using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPlatform : MonoBehaviour, IHookable {

    private Animator _animator;
	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void HookAction()
    {
        _animator.SetTrigger("open");
        Debug.Log("Hook Action!");

    }
}
