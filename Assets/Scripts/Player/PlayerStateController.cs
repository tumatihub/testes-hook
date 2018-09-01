using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour {

    // Public
    [HideInInspector] public float moveInput;
    [HideInInspector] public Rigidbody2D rb;
    public float speed;
    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce;
    public float jumpTime;
    public Transform hook;
    [HideInInspector] public LineRenderer hookLine;
    [HideInInspector] public Vector2 whereToShoot;
    public Camera cam;
    
    //Hook
    public float speedHook;
    [HideInInspector] public bool hookIsRetracted;
    [HideInInspector] public bool isHooked;
    [HideInInspector] public bool isRetracting;
    [HideInInspector] public bool isRetracted = true;
    [HideInInspector] public IHookable hookedObject;

    // States
    [SerializeField] private State _state;
    public State movingState;
    public State jumpingState;
    public State shootingState;
    public State hookedState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _state = movingState;
        hookLine = GetComponent<LineRenderer>();
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

    public Vector2 GetWhereToShoot()
    {
        Vector2 mousePos = new Vector2(
                    cam.ScreenToWorldPoint(Input.mousePosition).x,
                    cam.ScreenToWorldPoint(Input.mousePosition).y
                );
        return (new Vector3(mousePos.x, mousePos.y, 0) - hook.position).normalized;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(feetPos.transform.position, checkRadius);
    }
}
