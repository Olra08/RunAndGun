using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float mHealth = 2f;

    public float aggroRange;
    public float speed;
    public float gravity;

    private Rigidbody2D mRigidbody;
    public PlayerController playerController;
    public Transform playerTransform;
    public GameObject explosion;

    private void Start()
    {
        mRigidbody = GetComponent<Rigidbody2D>();
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
                StopChasingHero();
            }
        }
        if (playerController == null)
        {
            StopChasingHero();
        }
    }

    private void ChasePlayer()
    {
        if (transform.position.x < playerTransform.position.x)
        {
            mRigidbody.velocity = new Vector2(-speed, gravity);
        }
        else
        {
            mRigidbody.velocity = new Vector2(-speed, gravity);
        }
    }

    private void StopChasingHero()
    {
        mRigidbody.velocity = new Vector2(0, gravity - 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            mHealth--;

            if (mHealth <= 0)
            {
                GameObject obj = Instantiate(explosion, transform);
                obj.transform.parent = null;
                Destroy(gameObject);
            }
        }

    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("VoidOut"))
        {
            Destroy(gameObject);
        }
    }
    */
}
