using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gamekit2D
{
    public class Hook : MonoBehaviour
    {

        public GameObject player;
        private StateManager playerScript;

        void Awake()
        {
            playerScript = player.GetComponent<StateManager>();
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
}