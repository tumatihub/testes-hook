using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour {

    // Public
    [HideInInspector] public float moveInput;
    [HideInInspector] public Rigidbody2D rb;
    public float speed;
    public bool facingRight = true;
    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce;
    public float jumpTime;

    // States
    private State _state;
    public State movingState;
    public State jumpingState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _state = movingState;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        _state.handle_input(this);
	}

    private void FixedUpdate()
    {
        _state.update(this);
    }

    public void Flip()
    {
        if ((!facingRight && moveInput > 0) || (facingRight && moveInput < 0))
        {
            facingRight = !facingRight;
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
        }
        
    }

    public void ChangeState(State nextState)
    {
        _state.onExit(this);
        _state = nextState;
        _state.onEnter(this);
    }
}
