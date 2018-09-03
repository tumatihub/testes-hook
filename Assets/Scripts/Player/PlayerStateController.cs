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
    public Animator animator;
    public float fallingSpeed;
    
    // Hook
    public float speedHook;
    public bool isHooked;
    public bool isRetracting;
    public bool isRetracted = true;
    public IHookable hookedObject;
    public float hookMaxDistance;
    public float speedHookPlayer;
    public float hookInertia;

    // Shadow
    public bool isInShadow;


    // States
    [SerializeField] private State _state;
    public State movingState;
    public State jumpingState;
    public State shootingState;
    public State hookedState;
    public State hidingState;
    public State retractingState;
    public State pullingState;
    public State fallingState;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _state = movingState;
        hookLine = GetComponent<LineRenderer>();
        animator = GetComponent<Animator>();
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
        StartCoroutine(ChangeStateCoroutine(nextState));
    }

    private IEnumerator ChangeStateCoroutine(State nextState)
    {
        yield return new WaitForEndOfFrame();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shadow"))
        {
            isInShadow = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Shadow"))
        {
            isInShadow = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = collision.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }
}
