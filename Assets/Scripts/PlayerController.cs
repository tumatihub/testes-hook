﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {

    public enum States { MOVING, SHOOTING };

    public States state = States.MOVING;

    public Camera cam;
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float moveInput;
    private bool isGrounded;
    public Transform feetPos;
    public Transform hook;
    public float checkRadius;
    public LayerMask whatIsGround;
    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    private bool facingRight = true;
    public float speedHook;
    private Vector3 whereToShoot;
    private bool isShooting = false;
    public bool isHooked = false;
    public bool isRetracted = true;
    public float speedHookPlayer;
    public float speedHookRetracting;
    public bool isRetracting = false;
    public float hookInertia;
    private LineRenderer hookLine;
    public float hookMaxDistance;
    private bool pushed = false;
    public bool hasFired = false;
    public GameObject lifeController;
    public LifeController life;

    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody2D>();
        hookLine = GetComponent<LineRenderer>();
        lifeController = GameObject.Find("LifeController");
        life = lifeController.GetComponent<LifeController>();
        life.initialPosition = transform.position;
        life.currentPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (state == States.SHOOTING)
        {
            // Atualizar o render e movimento do hook
            if (isShooting)
            {
                if (isHooked)
                {
                    rb.isKinematic = true;
                    transform.Translate(whereToShoot * speedHookPlayer * Time.fixedDeltaTime, Space.World);
                }
                else if(!isRetracting)
                {
                    hook.Translate(whereToShoot * speedHook * Time.fixedDeltaTime, Space.World);
                    if (Vector2.Distance(transform.position, hook.transform.position) >= hookMaxDistance)
                    {
                        isHooked = false;
                        isRetracting = true;
                    }
                }
                
                if (isRetracting)
                {
                    rb.isKinematic = false;
                    if (!isGrounded && !pushed)
                    {
                        Debug.Log("(" + whereToShoot.x + "," + whereToShoot.y + ")");
                        rb.AddForce(whereToShoot * hookInertia * Time.fixedDeltaTime, ForceMode2D.Impulse);
                        pushed = true;
                    }
                    hook.Translate((transform.position - hook.transform.position).normalized * speedHookRetracting * Time.fixedDeltaTime, Space.World);
                }
            }

            if (isRetracted)
            {
                Debug.Log("Retracted");
                hook.position = transform.position;
                hookLine.enabled = false;
                hook.parent = transform;
                isShooting = false;
                isRetracting = false;
                hasFired = false;
                rb.isKinematic = false;
                rb.velocity = new Vector2(0, 0);
                pushed = false;
                state = States.MOVING;
            }

            // Render Hook
            hookLine.SetPosition(0, transform.position);
            hookLine.SetPosition(1, hook.transform.position);
        }
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        if (state == States.MOVING)
        {
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter = jumpTime;
            }

            if (Input.GetKey(KeyCode.Space) && isJumping)
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumping = false;
            }

            moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        }

        if (Input.GetButtonDown("Fire1") && isGrounded)
        {
            // Atirar hook na direção clicada
            if (!isShooting)
            {
                hookLine.enabled = true;
                rb.velocity = Vector2.zero;
                state = States.SHOOTING;
                hook.parent = null;
                isShooting = true;
                isRetracted = false;
                Vector2 mousePos = new Vector2(
                    cam.ScreenToWorldPoint(Input.mousePosition).x,
                    cam.ScreenToWorldPoint(Input.mousePosition).y
                );
                Debug.Log("(" + mousePos.x + "," + mousePos.y + ")");
                whereToShoot = (new Vector3(mousePos.x, mousePos.y, 0) - hook.position).normalized;
            }
        }

        if (Input.GetButtonDown("Fire2") && hasFired)
        {
            hasFired = false;
            // Recolher o hook
            isHooked = false;
            if (isShooting)
            {
                isRetracting = true;
            }
        }

        if (!facingRight && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight && moveInput < 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }

    void OnDrawGizmosSelected()
    {
        
        Gizmos.DrawWireSphere(feetPos.position, checkRadius);
    }
    public void Damaged(int damage)
    {
        life.OnDamage(damage);
        transform.position = life.currentPosition;
    }
    public void OnCheckpoint(Vector2 checkpoint)
    {
        life.currentPosition = checkpoint;
    }

    public void Respawn()
    {
        if(life.health > 0)
        {

        }
        else
        {

        }
    }
  
    
}
