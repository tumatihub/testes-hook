using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaptorPatrolBehavior : MonoBehaviour {

    public enum States { PATROLLING, FOLLOWING, ATTACKING }
    public States state = States.PATROLLING;
    public float speed = 2;
    public bool movingRight = true;
    public GameObject player;
    private Animator anima;
    private AggroBox aggroScript;
    private ObstacleDetection obstacle;

    // Use this for initialization
    void Start()
    {
        anima = GetComponent<Animator>();
        aggroScript = GetComponentInChildren<AggroBox>();
        obstacle = GetComponentInChildren<ObstacleDetection>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        switch (state)
        {
            case States.PATROLLING:
                if (aggroScript.aggro == true)
                {
                    state = States.FOLLOWING;
                    break;
                }
                if (this.GetComponentInChildren<ObstacleDetection>().obstacle == true)
                {
                    movingRight = !movingRight;

                }                               
                if (movingRight == true)
                {
                transform.eulerAngles = new Vector3(0, -180, 0);
                }
                else
                {
                transform.eulerAngles = new Vector3(0, 0, 0);
                }
                anima.SetBool("Idle", false);
                anima.SetBool("Running", true);
                transform.Translate(Vector2.left * speed * Time.deltaTime);
                break;
            case States.FOLLOWING:
                if (!aggroScript.aggro)
                {
                    state = States.PATROLLING;
                    return;
                }
                if (player.transform.position.x > transform.position.x - 2.5f && player.transform.position.x < transform.position.x + 2.5f)
                {
                    if (player.transform.position.x > transform.position.x)
                    {
                        movingRight = true;
                        transform.eulerAngles = new Vector3(0, -180, 0);
                    }
                    else if (player.transform.position.x < transform.position.x)
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
                    transform.Translate(Vector2.left * (speed * 2) * Time.deltaTime);
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
