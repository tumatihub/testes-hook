using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageControl : MonoBehaviour {
    public GameObject Player;
    public int damage = 1;
	
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.GetComponent<PlayerController>().Damaged(damage);
        }
    }
}
