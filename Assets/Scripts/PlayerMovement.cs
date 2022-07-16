using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public PlayerController controller;
	public Animator animator;
	public Rigidbody2D mRigidbody;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
	bool isDead = false;

    void Update()
	{
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Stand"))
        {
			animator.SetBool("z_pressed", true);
			runSpeed = 0f;
        }

		if (Input.GetButtonUp("Stand"))
		{
			animator.SetBool("z_pressed", false);
			animator.SetBool("Stand_ur", false);
			animator.SetBool("Stand_dr", false);
			runSpeed = 40f;
		}

		if (Input.GetAxisRaw("Horizontal") == 1 && Input.GetAxisRaw("Vertical") == 1)
		{
			if (animator.GetBool("z_pressed"))
            {
				animator.SetBool("Stand_u", false);
				animator.SetBool("Stand_ur", true);
			}
            else
            {
				animator.SetBool("Stand_u", false);
				animator.SetBool("Stand_ur", false);
				animator.SetBool("Walk_UR", true);
			}
        }

		if (Input.GetAxisRaw("Horizontal") == -1 && Input.GetAxisRaw("Vertical") == 1)
		{
			if (animator.GetBool("z_pressed"))
            {
				animator.SetBool("Stand_u", false);
				animator.SetBool("Stand_ur", true);
			}
            else
            {
				animator.SetBool("Stand_u", false);
				animator.SetBool("Stand_ur", false);
				animator.SetBool("Walk_UR", true);
			}
		}

		if (Input.GetAxisRaw("Horizontal") == 1 && Input.GetAxisRaw("Vertical") == -1)
		{
			if (animator.GetBool("z_pressed"))
            {
				animator.SetBool("Stand_d", false);
				animator.SetBool("Stand_dr", true);
            }
            else
            {
				crouch = false;
				animator.SetBool("Walk_DR", true);
			}
		}

		if (Input.GetAxisRaw("Horizontal") == -1 && Input.GetAxisRaw("Vertical") == -1)
		{
			if (animator.GetBool("z_pressed"))
            {
				animator.SetBool("Stand_d", false);
				animator.SetBool("Stand_dr", true);
			}
            else
            {
				crouch = false;
				animator.SetBool("Walk_DR", true);
			}
		}

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("IsJumping", true);
		}

		if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == -1)
		{
			if (animator.GetBool("z_pressed"))
            {
				animator.SetBool("Stand_dr", false);
				animator.SetBool("Stand_d", true);
            }
            else
            {
				crouch = true;
			}
		}

		if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 1)
		{
			animator.SetBool("Stand_ur", false);
			animator.SetBool("Stand_u", true);
		}

		if (Input.GetAxisRaw("Vertical") == 0)
		{
			animator.SetBool("Walk_UR", false);
			animator.SetBool("Walk_DR", false);
			animator.SetBool("Stand_u", false);
			animator.SetBool("Stand_ur", false);
			animator.SetBool("Stand_dr", false);
			animator.SetBool("Stand_d", false);
			crouch = false;
		}

		if (!animator.GetBool("IsJumping") && !controller.m_Grounded && !animator.GetBool("IsHit"))
        {
			animator.SetBool("IsFalling", true);
        }
        else
        {
			animator.SetBool("IsFalling", false);
        }
	}

	public void OnLanding()
    {
		animator.SetBool("IsJumping", false);
	}

	public void OnCrouching(bool isCrouching)
    {
		animator.SetBool("IsCrouching", isCrouching);
    }

	void FixedUpdate()
	{
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.CompareTag("Hit"))
		{
			animator.SetBool("IsHit", true);
			//Destroy(gameObject);
		}
	}*/

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Hit") && !isDead)
		{
			if (transform.localScale.x == 1)
            {
				mRigidbody.AddForce(new Vector2(-800f, 600f));
				animator.SetBool("IsHit", true);
				isDead = true;
				//Destroy(gameObject);
			}else if (transform.localScale.x == -1)
            {
				mRigidbody.AddForce(new Vector2(800f, 600f));
				animator.SetBool("IsHit", true);
				isDead = true;
				//Destroy(gameObject);
			}
		}
	}
}
