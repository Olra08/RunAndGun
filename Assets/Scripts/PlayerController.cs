using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	public bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	
	public GameObject bullet;
	private Transform mGunPointer;
	private float cooldown = 0f;
	private AudioSource mAudioSource;
	public PlayerMovement movement;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();

		if (PlayerPrefs.GetInt("checkpoint") == 0)
        {
			transform.position = new Vector3(-209.5f, -4.23f);
		}
		if (PlayerPrefs.GetInt("checkpoint") == 1)
        {
			transform.position = new Vector3(-118.9f, -4.23f);
        }
		if (PlayerPrefs.GetInt("checkpoint") == 2)
        {
			transform.position = new Vector3(-27.56f, -4.23f);
		}
	}

    private void Start()
    {
		mGunPointer = transform.Find("GunPointer");
		mAudioSource = GetComponent<AudioSource>();
	}

    private void Update()
    {
		if (!movement.GetDead())
        {
			if (bullet.name != "Default")
			{
				if (Input.GetButton("Fire1"))
				{
					cooldown -= Time.deltaTime;
					if (cooldown <= 0f)
					{
						Fire();
					}
				}
				if (Input.GetButtonUp("Fire1"))
				{
					cooldown = 0f;
				}
			}
			else
			{
				if (Input.GetButtonDown("Fire1"))
				{
					Fire();
				}
			}
		}
	}

    private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}

	public void Move(float move, bool crouch, bool jump)
	{
		// If crouching, check to see if the character can stand up
		/*if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}*/

		//only control the player if grounded or airControl is turned on
		
		if (m_Grounded || m_AirControl)
		{

			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			}
			else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}

			if (move == 0 && !m_FacingRight && Input.GetAxisRaw("Horizontal") == 1 && !movement.GetDead())
			{
				// ... flip the player.
				Flip();
			}
			if (move == 0 && m_FacingRight && Input.GetAxisRaw("Horizontal") == -1 && !movement.GetDead())
			{
				// ... flip the player.
				Flip();
			}

		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			// Add a vertical force to the player.
			m_Grounded = true;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}

	private void Fire()
	{
		if (bullet.name == "Spread")
        {
			/*mAudioSource.clip = Resources.Load<AudioClip>("spread");
			mAudioSource.Play();*/
			mAudioSource.PlayOneShot(Resources.Load<AudioClip>("spread"));
			GameObject obj = Instantiate(bullet, mGunPointer);
			obj.transform.parent = null;
			cooldown = 0.18f;
		} else if (bullet.name == "MachineGun")
        {
			//mAudioSource.clip = Resources.Load<AudioClip>("default");
			//mAudioSource.Play();
			mAudioSource.PlayOneShot(Resources.Load<AudioClip>("default"));
			GameObject obj = Instantiate(bullet, mGunPointer);
			obj.transform.parent = null;
			cooldown = 0.06f;
        } else if (bullet.name == "Laser")
        {
			mAudioSource.clip = Resources.Load<AudioClip>("laser");
			mAudioSource.Play();
			//mAudioSource.PlayOneShot(Resources.Load<AudioClip>("laser"));
			GameObject obj = Instantiate(bullet, mGunPointer);
			obj.transform.parent = null;
			cooldown = 0.80f;
		}
        else
        {
			/*mAudioSource.clip = Resources.Load<AudioClip>("default");
			mAudioSource.Play();*/
			mAudioSource.PlayOneShot(Resources.Load<AudioClip>("default"));
			GameObject obj = Instantiate(bullet, mGunPointer);
			obj.transform.parent = null;
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.CompareTag("IconS"))
		{
			ItemGet();
			bullet = Resources.Load<GameObject>("Spread");
		}
		if (collision.gameObject.CompareTag("IconM"))
		{
			ItemGet();
			bullet = Resources.Load<GameObject>("MachineGun");
		}
		if (collision.gameObject.CompareTag("IconL"))
		{
			ItemGet();
			bullet = Resources.Load<GameObject>("Laser");
		}
		if (collision.gameObject.CompareTag("IconB"))
		{
			/*mAudioSource.clip = Resources.Load<AudioClip>("bomb");
			mAudioSource.Play();*/
			mAudioSource.PlayOneShot(Resources.Load<AudioClip>("bomb"));
			//bullet = Resources.Load<GameObject>("Spread");
		}
	}

	private void ItemGet()
    {
		/*mAudioSource.clip = Resources.Load<AudioClip>("item");
		mAudioSource.Play();*/
		mAudioSource.PlayOneShot(Resources.Load<AudioClip>("item"));
	}
}
