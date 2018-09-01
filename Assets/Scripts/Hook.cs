using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {

    public GameObject player;
    private PlayerStateController _controller;

    void Start()
    {
        _controller = player.GetComponent<PlayerStateController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hook colidiu com Player!");
            _controller.isRetracted = true;
        }

        if (collision.gameObject.tag == "Hookable" || collision.gameObject.tag == "HookSupport")
        {
            _controller.isHooked = true;
            _controller.hookedObject = collision.gameObject.GetComponent<IHookable>();
        }
        else if (collision.gameObject.tag != "Player"  && collision.gameObject.tag != "Aggro" && collision.gameObject.tag != "Shadow")
        {
            _controller.isHooked = false;
            _controller.isRetracting = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _controller.isRetracted = false;
        }

        if (collision.gameObject.tag == "Hookable" || collision.gameObject.tag == "HookSupport")
        {
            _controller.isHooked = false;
            _controller.hookedObject = null;
        }
    }
}
