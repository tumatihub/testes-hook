using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }


        private void Update()
        {
            if (!m_Jump && m_Character.state == PlatformerCharacter2D.States.MOVING )
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (Input.GetMouseButton(0) && !m_Jump)
            {
                m_Character.state = PlatformerCharacter2D.States.SHOOTING_HOOK;
            }
            else
            {
                m_Character.state = PlatformerCharacter2D.States.MOVING;
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.

            if ( m_Character.state == PlatformerCharacter2D.States.MOVING)
            {
                bool crouch = Input.GetKey(KeyCode.LeftControl);
                float h = CrossPlatformInputManager.GetAxis("Horizontal");
                // Pass all parameters to the character control script.
                m_Character.Move(h, crouch, m_Jump);
                m_Jump = false;
            }
            
        }
    }
}
