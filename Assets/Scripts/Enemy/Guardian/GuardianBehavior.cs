using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianBehavior : MonoBehaviour {
    public enum States { GUARDING, FOLLOWING, ATTACKING, RETURNING }
    public States state = States.GUARDING;
    public float speed = 2;
    public bool movingRight = true;
    public GameObject player;
    private Animator anima;
    private AggroBox aggroScript;
    private Vector2 waypoint;
    private ObstacleDetection obstacle;

	// Use this for initialization
	void Start () {
        anima = GetComponent<Animator>();
        aggroScript = GetComponentInChildren<AggroBox>();
        waypoint = transform.position;
        obstacle = GetComponentInChildren<ObstacleDetection>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void FixedUpdate()
    {
        switch (state)
        {
            case States.GUARDING:
                if(aggroScript.aggro == true)
                {
                    state = States.FOLLOWING;
                    break;
                }
                if(transform.position.x < waypoint.x +0.5f && transform.position.x > waypoint.x -0.5f)
                {
                    anima.SetBool("Idle", true);
                    anima.SetBool("Running", false);
                    if(movingRight == true)
                    {
                        transform.eulerAngles = new Vector3(0, -180, 0);
                    }else
                    {
                        transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                }
                break;
            case States.RETURNING:
                if (aggroScript.aggro == true)
                {
                    state = States.FOLLOWING;
                    break;
                }
                if (transform.position.x < waypoint.x + 0.5f && transform.position.x > waypoint.x - 0.5f)
                {
                    state = States.GUARDING;
                    break;
                }
                if (transform.position.x > waypoint.x + 1)
                {
                    movingRight = false;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (transform.position.x < waypoint.x - 1)
                {
                    movingRight = true;
                    transform.eulerAngles = new Vector3(0, -180, 0);
                }
                anima.SetBool("Idle", false);
                anima.SetBool("Running", true);
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                break;
            case States.FOLLOWING:
                if (!aggroScript.aggro)
                {
                    state = States.RETURNING;
                    return;
                }
                if (player.transform.position.x > transform.position.x - 2.5f && player.transform.position.x < transform.position.x + 2.5f)
                {
                    if(player.transform.position.x > transform.position.x)
                    {
                        movingRight = true;
                        transform.eulerAngles = new Vector3(0, -180, 0);
                    }
                    else if(player.transform.position.x < transform.position.x)
                    {
                        movingRight = false;
                        transform.eulerAngles = new Vector3(0, 0, 0);
                    }
                    state = States.ATTACKING;
                    return;
                }
                if (player.transform.position.x > transform.position.x + 2)
                {
                    movingRight = true;
                    transform.eulerAngles = new Vector3(0, -180, 0);
                }
                if (player.transform.position.x < transform.position.x - 2)
                {
                    movingRight = false;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                if (obstacle.obstacle == false)
                {
                    anima.SetBool("Idle", false);
                    anima.SetBool("Running", true);
                    transform.Translate(Vector2.left * (speed * 1.5f) * Time.deltaTime);
                }
                else
                {
                    anima.SetBool("Idle", true);
                    anima.SetBool("Running", false);
                }
                break;
            case States.ATTACKING:
                anima.SetBool("Idle", false);
                anima.SetBool("Running", false);
                anima.SetBool("Attack", true);
                break;
                
        }
    }
    private void Attacked()
    {
        anima.SetBool("Attack", false);
        state = States.FOLLOWING;
    }
}
