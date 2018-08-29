using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour {
    public int health = 3;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnDamage(int damage)
    {
        health =- damage;
    }

    public void OnlifeGain(int life)
    {
        health = +life;
    }
}
