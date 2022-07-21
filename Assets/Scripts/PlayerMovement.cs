using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
	public PlayerController controller;
	public Animator animator;
	public Rigidbody2D mRigidbody;
	private Vector3 mVector3 = new Vector3(1f, 0f, 0f);
	private BoxCollider2D mCollider;
	public Spawner lockL;
	public Spawner lockR;
	public Transform blackScreen;
	public Animator black;

	public float runSpeed;

	float horizontalMove = 0f;
	bool jump = false;
	bool crouch = false;
	bool isDead = false;

	private int Lives = 4;

	public GameObject boss;
	public CinemachineVirtualCamera cinemachine;
	private CinemachineImpulseSource impulse;
	public AudioSource mGameMusic;
	private AudioSource mAudioSource;

	private void Start()
    {
		impulse = transform.GetComponent<CinemachineImpulseSource>();
		mCollider = transform.GetComponent<BoxCollider2D>();
		mAudioSource = GetComponent<AudioSource>();
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
				runSpeed = 25f;
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
	}

	public void OnCrouching(bool isCrouching)
    {
		animator.SetBool("IsCrouching", isCrouching);
    }

	void FixedUpdate()
	{
		if (!isDead)
        {
			controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
			jump = false;
		}
		if (isDead)
        {
			controller.Move(0f, false, false);
        }
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
				//mRigidbody.AddForce(new Vector2(-800f, 600f));
				animator.SetBool("IsHit", true);
				//Destroy(gameObject);
			}
			else if (transform.localScale.x == -1)
            {
				//mRigidbody.AddForce(new Vector2(800f, 600f));
				animator.SetBool("IsHit", true);
				//Destroy(gameObject);
			}
			horizontalMove = 0f;
			isDead = true;
		}

		if (collision.gameObject.CompareTag("ScreenChange"))
        {
			lockL.enabled = false;
			lockR.enabled = false;
			StartCoroutine(FadeAudioSource.StartFade(mGameMusic, 5f, 0f));
			StartCoroutine(BossEntrance());
			cinemachine.Priority += 1;
			Invoke(nameof(Shake), 5f);
		}
		if (collision.gameObject.CompareTag("StopSpawn"))
        {
			lockL.enabled = false;
			lockR.enabled = false;
		}

		if (collision.gameObject.CompareTag("SpawnEnemy"))
		{
			lockL.enabled = true;
			lockR.enabled = true;
		}
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
		if (collision.gameObject.CompareTag("LockL"))
		{
			transform.position = new Vector3(transform.position.x + 0.2f, transform.position.y);
		}
		if (collision.gameObject.CompareTag("LockR"))
		{
			transform.position = new Vector3(transform.position.x - 0.2f, transform.position.y);
		}
	}

	IEnumerator BossEntrance()
	{
		yield return new WaitForSeconds(5);
		StartCoroutine(FadeAudioSource.StartFade(mGameMusic, 1f, 0.8f));
		//mGameMusic.PlayOneShot(Resources.Load<AudioClip>("Boss&Quake"));
		mGameMusic.clip = Resources.Load<AudioClip>("Boss&Quake");
		mGameMusic.Play();
		yield return new WaitForSeconds(5);
		boss.SetActive(true);
	}

	private void Shake()
    {
		impulse.GenerateImpulse(5f);
	}

	public bool GetDead()
    {
		return isDead;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.CompareTag("Hit"))
		{
			//isDead = true;
			if (Lives != 0)
			{
				gameObject.layer = 8;
				animator.SetBool("IsHit", true);
				isDead = true;
				StartCoroutine(TimeAfterDying());
			}
			if (Lives <= 0)
            {
                StartCoroutine(TimeAfterDying());
				gameObject.layer = 8;
				animator.SetBool("IsHit", true);
				isDead = true;
				blackScreen.gameObject.SetActive(true);
				black.SetBool("GameOver", true);
				StartCoroutine(ChangeScreen());
            }
		}
	}

	IEnumerator TimeAfterDying()
    {
		if (Lives != 0)
        {
			mAudioSource.clip = Resources.Load<AudioClip>("death");
			mAudioSource.Play();
			yield return new WaitForSeconds(2);
			gameObject.transform.position = new Vector3(transform.position.x, -4.23f);
			Lives--;
			animator.SetBool("IsHit", false);
			isDead = false;
			//bullet = Resources.Load<GameObject>("Default");
			yield return new WaitForSeconds(4f);
			gameObject.layer = 6;
        }
        else
        {
			mAudioSource.clip = Resources.Load<AudioClip>("death");
			mAudioSource.Play();
			yield return new WaitForSeconds(2);
		}
	}

	IEnumerator ChangeScreen()
    {
		yield return new WaitForSeconds(3);
		Scene scene = SceneManager.GetActiveScene();
		if (scene.name == "HardScene")
        {
			SceneManager.LoadScene("LoseHScene");
        }
        else
        {
			SceneManager.LoadScene("LoseScene");
		}
	}
}
