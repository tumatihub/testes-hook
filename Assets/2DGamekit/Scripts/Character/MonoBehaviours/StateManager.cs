using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamekit2D
{


    public class StateManager : MonoBehaviour
    {
        private PlayerCharacter m_PlayerCharacter;
        private CharacterController2D m_CharacterController2D;
        public enum States { MOVING, SHOOTING };

        public States state = States.MOVING;

        public Camera cam;
        private Rigidbody2D rb;
        
        
        
        public Transform hook;
        public Transform hookHolder;
        
        
        
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

        // Use this for initialization
        void Start()
        {
            m_PlayerCharacter = GetComponent<PlayerCharacter>();
            m_CharacterController2D = GetComponent<CharacterController2D>();
            rb = GetComponent<Rigidbody2D>();
            hookLine = GetComponent<LineRenderer>();
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
                        //rb.AddForce(whereToShoot * hookInertia * Time.fixedDeltaTime, ForceMode2D.Impulse);
                    }
                    else if (!isRetracting)
                    {
                        hook.Translate(whereToShoot * speedHook * Time.fixedDeltaTime, Space.World);
                        if (Vector2.Distance(hookHolder.position, hook.transform.position) >= hookMaxDistance)
                        {
                            isHooked = false;
                            isRetracting = true;
                        }
                    }

                    if (isRetracting)
                    {
                        rb.isKinematic = false;
                        if (!m_CharacterController2D.IsGrounded && !pushed)
                        {
                            Debug.Log("(" + whereToShoot.x + "," + whereToShoot.y + ")");
                            rb.AddForce(whereToShoot * hookInertia * Time.fixedDeltaTime, ForceMode2D.Impulse);
                            pushed = true;
                        }
                        hook.Translate((hookHolder.position - hook.transform.position).normalized * speedHookRetracting * Time.fixedDeltaTime, Space.World);
                    }
                }

                if (isRetracted)
                {
                    Debug.Log("Retracted");
                    hook.position = hookHolder.position;
                    hookLine.enabled = false;
                    hook.parent = transform;
                    isShooting = false;
                    isRetracting = false;
                    rb.isKinematic = false;
                    rb.velocity = new Vector2(0, 0);
                    pushed = false;
                    state = States.MOVING;
                }

                // Render Hook
                hookLine.SetPosition(0, hookHolder.position);
                hookLine.SetPosition(1, hook.transform.position);
            }
        }

        void Update()
        {
            

            if (state == States.MOVING)
            {
                m_PlayerCharacter.maxSpeed = 7;
                m_PlayerCharacter.jumpSpeed = 16.5f;
            }
            if (state == States.SHOOTING)
            {                
                m_PlayerCharacter.maxSpeed = 0;
                m_PlayerCharacter.jumpSpeed = 0;
            }

            if (Input.GetButtonDown("Fire1") && m_CharacterController2D.IsGrounded)
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

            if (Input.GetButtonDown("Fire2"))
            {
                // Recolher o hook
                isHooked = false;
                if (isShooting)
                {
                    isRetracting = true;
                }
            }

            
        }

        
    }
}