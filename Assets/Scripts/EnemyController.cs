using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    private float mHealth;

    public float aggroRange;
    public float speed;
    public float gravity;

    private Rigidbody2D mRigidbody;
    public Animator animator;
    public float raycastDistance = 0.3f;
    private PlayerController playerController;
    private Transform playerTransform;
    public GameObject explosion;
    private bool isRight = true;
    private bool isLeft = true;
    private bool walkR = false;
    private bool walkL = false;
    private float mTimer = 0f;
    private float timetoDestroy = 10f;

    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "HardScene")
        {
            mHealth = 5f;
            speed = 12.5f;
        }
        else
        {
            mHealth = 2f;
        }
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
        mTimer += Time.deltaTime;
        if (mTimer > timetoDestroy)
        {
            Destroy(gameObject);
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
        } else if (transform.position.x > playerTransform.position.x && isLeft || walkL)
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
            mHealth--;
            if (mHealth <= 0)
            {
                GameObject obj = Instantiate(explosion, transform);
                obj.transform.parent = null;
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("BulletOneHit"))
        {
            GameObject obj = Instantiate(explosion, transform);
            obj.transform.parent = null;
            Destroy(gameObject);
        }
    }
}
