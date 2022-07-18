using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public PlayerController controller;
	public Animator animator;
	public Rigidbody2D mRigidbody;
	private Transform transformGunPointer;
	private Vector3 mVector3 = new Vector3(1f, 0f, 0f);

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
	bool isDead = false;

    private void Start()
    {
		transformGunPointer = transform.Find("GunPointer");

	}

    void Update()
	{
		if (!isDead)
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

			//UR
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

			//UR
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

			//DR
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

			//DR
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
			PointerDirection();

			if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == -1)
			{
				if (animator.GetBool("z_pressed"))
				{
					animator.SetBool("Stand_dr", false);
					animator.SetBool("Stand_d", true);
				}
				else
				{
					if (!animator.GetBool("IsJumping") || !animator.GetBool("IsFalling"))
                    {
						crouch = true;
						animator.SetBool("Stand_d", false);
					}
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
				animator.SetBool("IsFalling", false);
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
	}

	public void OnLanding()
    {
		animator.SetBool("IsJumping", false);
		Debug.Log("pise el suelo");
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


	public Vector3 GetDirection()
	{
		return mVector3;
	}

	private void PointerDirection()
    {
		if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
			if (transform.localScale.x == 1)
			{
				mVector3 = new Vector3(1f, 0f, 0f);
			}
			else if (transform.localScale.x == -1)
			{
				mVector3 = new Vector3(-1f, 0f, 0f);
			}
		}

		//Up
		if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 1)
		{
			mVector3 = new Vector3(0f, 1f, 0f);
		}
		//UpRight
		if (Input.GetAxisRaw("Horizontal") == 1 && Input.GetAxisRaw("Vertical") == 1)
		{
			mVector3 = new Vector3(1f, 1f, 0f);
		}
		//Right
		if (Input.GetAxisRaw("Horizontal") == 1 && Input.GetAxisRaw("Vertical") == 0)
		{
			mVector3 = new Vector3(1f, 0f, 0f);
		}
		//DownRight
		if (Input.GetAxisRaw("Horizontal") == 1 && Input.GetAxisRaw("Vertical") == -1)
		{
			mVector3 = new Vector3(1f, -1f, 0f);
		}
		//Down
		if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == -1)
		{
			if (animator.GetBool("z_pressed"))
            {
				mVector3 = new Vector3(0f, -1f, 0f);
			} else if (!animator.GetBool("z_pressed") && transform.localScale.x == 1)
            {
				mVector3 = new Vector3(1f, 0f, 0f);
			} else if (!animator.GetBool("z_pressed") && transform.localScale.x == -1)
            {
				mVector3 = new Vector3(-1f, 0f, 0f);
			}
			if (animator.GetBool("IsJumping"))
            {
				mVector3 = new Vector3(0f, -1f, 0f);
			}

		}
		//DownLeft
		if (Input.GetAxisRaw("Horizontal") == -1 && Input.GetAxisRaw("Vertical") == -1)
		{
			mVector3 = new Vector3(-1f, -1f, 0f);
		}
		//Left
		if (Input.GetAxisRaw("Horizontal") == -1 && Input.GetAxisRaw("Vertical") == 0)
		{
			mVector3 = new Vector3(-1f, 0f, 0f);
		}
		//UpLeft
		if (Input.GetAxisRaw("Horizontal") == -1 && Input.GetAxisRaw("Vertical") == 1)
		{
			mVector3 = new Vector3(-1f, 1f, 0f);
		}
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Hit") && !isDead)
		{
			if (transform.localScale.x == 1)
            {
				mRigidbody.AddForce(new Vector2(-800f, 600f));
				animator.SetBool("IsHit", true);
				//Destroy(gameObject);
			}
			else if (transform.localScale.x == -1)
            {
				mRigidbody.AddForce(new Vector2(800f, 600f));
				animator.SetBool("IsHit", true);
				//Destroy(gameObject);
			}
			horizontalMove = 0f;
			isDead = true;
		}
	}
	
	/*private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.CompareTag("Hit"))
		{
			animator.SetBool("IsHit", true);
			//Destroy(gameObject);
		}
	}*/
}
