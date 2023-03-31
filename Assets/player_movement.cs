using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unitygame
{

    public class player_movement : MonoBehaviour
    {

        public Animator animator;

        // viktiga variablar
        private float horizontal;
        private float speed = 3f;
        private float jumpingForce = 16f;
        private float crouchSpeed = 1f;
        private bool isFacingRight = true;
        private bool isCrouching = false;
        private float slopeForce = 2f;
        private float slopeForceRayLength = 2f;

        //refering till rigidbody
        [SerializeField] private Rigidbody2D rigidb;
        [SerializeField] private Transform groundCheckish;
        [SerializeField] private LayerMask groundLayer;
       
        void Update()
        {
            // variablen som ger ut -1, 0 eller 1 i v�rde som indikerar vad f�r riktning 
            horizontal = Input.GetAxisRaw("Horizontal") * speed;

            animator.SetFloat("speed 0", Mathf.Abs(horizontal));

            // till knapptryckiong av hoppknappar
            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                rigidb.velocity = new Vector2(rigidb.velocity.x, jumpingForce);
                animator.SetBool("is jumping", true); // g�r "is jumping" till true
            }
            else if (IsGrounded()) // n�r player landar
            {
                animator.SetBool("is jumping", false); // g�r "is jumping" till false
            }

            if (Input.GetButtonUp("Jump") && rigidb.velocity.y > 0f)
            {
                rigidb.velocity = new Vector2(rigidb.velocity.x, rigidb.velocity.y * 0.5f);
            }
            // till knapptryckiong av crouch
            if (Input.GetButton("Crouch"))
            {
                isCrouching = true;
                animator.SetBool("is crouching", true); // g�r "is crouching" till true
                animator.SetBool("isCrouch", true); //g�r samma som �vre
            }
            else
            {
                isCrouching = false;
                animator.SetBool("is crouching", false); // g�r "is crouching" till false
                animator.SetBool("isCrouch", false); // samma som �vre
            }

            animator.SetBool("isCrouch", isCrouching);

            Flip();
        }


        private void FixedUpdate()
        {
            // �ndrar ens speed n�r man crouchar s� man �r l�ngsammare
            float currentSpeed = speed;

            if (isCrouching)
            {
                currentSpeed = crouchSpeed;
            }
            //fixar s� man kan g� p� slopes
            Vector2 direction = new Vector2(horizontal, 0);
            RaycastHit2D hit = Physics2D.Raycast(groundCheckish.position, Vector2.down, slopeForceRayLength, groundLayer);
            if (hit.collider != null)
            {
                float angle = Vector2.Angle(hit.normal, Vector2.up);
                if (angle > 45f)
                {
                    direction = Quaternion.Euler(0, 0, -90) * hit.normal * slopeForce;
                }
            }

            rigidb.velocity = new Vector2(direction.x * currentSpeed, rigidb.velocity.y);
        }

        private bool IsGrounded()
        {
            //variable som ska vara true f�r att man ska kunna hooppa
            return Physics2D.OverlapCircle(groundCheckish.position, 1f, groundLayer);
        }

        private void Flip()
        {
            //��ndrar h�llet man tittar p� beroende p� tidigare variable
            if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
        }
    }
}
