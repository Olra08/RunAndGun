using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleController : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    public float aggroRange;
    public float speed;
    public float gravity;

    private Rigidbody2D mRigidbody;
    private PlayerController playerController;
    private Transform playerTransform;
    private bool isRight = true;
    private bool isLeft = true;
    private bool walkR = false;
    private bool walkL = false;

    private void Start()
    {
        boxCollider = transform.GetComponent<BoxCollider2D>();
        mRigidbody = GetComponent<Rigidbody2D>();
        playerController = GameManager.GetInstance().player;
        playerTransform = GameManager.GetInstance().player.transform;
    }

    private void Update()
    {
        if (playerController != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer < aggroRange)
            {
                ChasePlayer();
            }
            else
            {
                ChasePlayer();
            }
        }
        if (playerController == null)
        {
            StopChasingHero();
        }
    }

    private void ChasePlayer()
    {
        if (transform.position.x < playerTransform.position.x && isRight || walkR)
        {
            mRigidbody.velocity = new Vector2(speed, gravity);
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            isLeft = false;
            walkR = true;
        }
        else if (transform.position.x > playerTransform.position.x && isLeft || walkL)
        {
            mRigidbody.velocity = new Vector2(-speed, gravity);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            isRight = false;
            walkL = true;
        }
    }

    private void StopChasingHero()
    {
        mRigidbody.velocity = new Vector2(0, gravity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            if (gameObject.CompareTag("CapsuleS"))
            {
                boxCollider.enabled = false;
                GameObject obj = Instantiate(Resources.Load<GameObject>("IconS"), transform);
                obj.transform.parent = null;
                gameObject.SetActive(false);
            }
            if (gameObject.CompareTag("CapsuleM"))
            {
                boxCollider.enabled = false;
                GameObject obj = Instantiate(Resources.Load<GameObject>("IconM"), transform);
                obj.transform.parent = null;
                gameObject.SetActive(false);
            }
            if (gameObject.CompareTag("CapsuleL"))
            {
                boxCollider.enabled = false;
                GameObject obj = Instantiate(Resources.Load<GameObject>("IconL"), transform);
                obj.transform.parent = null;
                gameObject.SetActive(false);
            }
            if (gameObject.CompareTag("CapsuleB"))
            {
                boxCollider.enabled = false;
                GameObject obj = Instantiate(Resources.Load<GameObject>("IconB"), transform);
                obj.transform.parent = null;
                gameObject.SetActive(false);
            }
        }
    }
}
