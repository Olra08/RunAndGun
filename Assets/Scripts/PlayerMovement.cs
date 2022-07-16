using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public PlayerController controller;
	public Animator animator;
	[SerializeField]
	private SpriteRenderer spriteRenderer;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;

    // Update is called once per frame
    void Update()
	{

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") == 1)
		{
			animator.SetBool("Walk_UR", true);
        }

		if (Input.GetAxisRaw("Vertical") == 0)
		{
			animator.SetBool("Walk_UR", false);
		}

		if (Input.GetAxisRaw("Horizontal") > 0 && Input.GetAxisRaw("Vertical") == -1)
		{
			animator.SetBool("Walk_DR", true);
		}

		if (Input.GetAxisRaw("Vertical") == 0)
		{
			animator.SetBool("Walk_DR", false);
		}

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			animator.SetBool("IsJumping", true);
		}

		if (Input.GetAxisRaw("Vertical") == -1 && Input.GetAxisRaw("Horizontal") != 1)
		{
			crouch = true;
		}
		else if (Input.GetAxisRaw("Vertical") == 0)
		{
			crouch = false;
		}

		if (Input.GetAxisRaw("Vertical") == 1 && Input.GetAxisRaw("Horizontal") != 1)
        {
			//spriteRenderer.sprite = 
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
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
		jump = false;
	}
}
