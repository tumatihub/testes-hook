using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour {
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerController>().OnCheckpoint(transform.position);
            Destroy(this.gameObject);
        }
    }
}
