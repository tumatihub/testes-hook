using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour {

    public GameObject player;
    private PlayerController playerScript;

    void Awake()
    {
        playerScript = player.GetComponent<PlayerController>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Hook colidiu com Player!");
            playerScript.isRetracted = true;
        }

        if (collision.gameObject.tag == "Hookable")
        {
            playerScript.isHooked = true;
        }
        else if (collision.gameObject.tag != "Player")
        {
            playerScript.isHooked = false;
            playerScript.isRetracting = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerScript.isRetracted = false;
        }

        if (collision.gameObject.tag == "Hookable")
        {
            playerScript.isHooked = false;
        }
    }
}
