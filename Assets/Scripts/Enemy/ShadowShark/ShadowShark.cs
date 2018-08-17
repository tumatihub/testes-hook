using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowShark : MonoBehaviour {
    public enum States {PATROLLING, FOLLOWING, ATTACKING}
    public States state = States.PATROLLING;
    public float speed=2;
    public bool movingRight = true;
    public Transform player;
    public GameObject aggroBox;
    private AggroBox aggroScript;
    private float height;
   
    
   
	// Use this for initialization
	void Start () {
        aggroScript = aggroBox.GetComponent<AggroBox>();
        height = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
       

    }
    private void FixedUpdate() {
        if (state == States.PATROLLING)
        {
            if (aggroScript.aggro == true)
            {
                state = States.FOLLOWING;
                return;
            }
            //checa se tem algum objeto em frente ou abismo e acerta a direção. 
            if (this.GetComponentInChildren<ObstacleDetection>().obstacle == true)
            {
                movingRight = !movingRight;

            }
            if (movingRight == false)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                
            }
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        else if(state == States.FOLLOWING)
        {

            if (player.position.x > transform.position.x -0.5f && player.position.x < transform.position.x +0.5f )
            {
                state = States.ATTACKING;
                return;
            }
                if (!aggroScript.aggro)
            {
                state = States.PATROLLING;
                return;
            }
            if (player.position.x > transform.position.x + 2)
            {
                movingRight = true;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            if (player.position.x < transform.position.x - 2)
            {
                movingRight = false;
                transform.eulerAngles = new Vector3(0, -180, 0);
            }
            transform.Translate(Vector2.right * (speed*1.5f) * Time.deltaTime);

        }
        else if (state == States.ATTACKING)
        {
            if (transform.position.y < height + 2)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.up * 2;
            }
            else
            {
                transform.position = new Vector2(transform.position.x, height);
                state = States.PATROLLING;
            }
        }
    }
   

}

