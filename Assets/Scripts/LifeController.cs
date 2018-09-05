using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour {
    public static LifeController  control;
    public int health = 3;
    public Vector2 currentPosition;
    public Vector2 initialPosition;


    // Use this for initialization
    private void Awake()
    {
        if(control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if(control != this)
        {
            Destroy(gameObject);
        }
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnDamage(int damage)
    {
        health = health - damage;
    }

    public void OnlifeGain(int life)
    {
        health = health + life;
    }
}
