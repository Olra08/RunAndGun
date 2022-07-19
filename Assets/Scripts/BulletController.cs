using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public float timeToDestroy;

    private Vector3 mDirection;
    private float mTimer = 0f;
    public GameObject spread1 = null;
    public GameObject spread2 = null;
    public GameObject spread3 = null;
    public GameObject spread4 = null;
    public GameObject spread5 = null;
    private Vector3 mDirection1;
    private Vector3 mDirection2;
    private Vector3 mDirection4;
    private Vector3 mDirection5;

    private void Start()
    {
        mDirection = GameManager.GetInstance().movement.GetDirection();

        if (spread1 != null)
        {
            if (mDirection.x == mDirection.y)
            {
                mDirection1 = mDirection + new Vector3(-0.30f, 0.30f, 0f);
                mDirection2 = mDirection + new Vector3(-0.15f, 0.15f, 0f);
                mDirection4 = mDirection + new Vector3(0.15f, -0.15f, 0f);
                mDirection5 = mDirection + new Vector3(0.30f, -0.30f, 0f);
                //Debug.Log("arriba derecha / abajo izquierda");
            }
            if (mDirection.x == -mDirection.y || -mDirection.x == mDirection.y)
            {
                mDirection1 = mDirection + new Vector3(0.30f, 0.30f, 0f);
                mDirection2 = mDirection + new Vector3(0.15f, 0.15f, 0f);
                mDirection4 = mDirection + new Vector3(-0.15f, -0.15f, 0f);
                mDirection5 = mDirection + new Vector3(-0.30f, -0.30f, 0f);
                //Debug.Log("diagonal");
            }
            if (mDirection.x == 0f && mDirection.y != 0f)
            {
                mDirection1 = mDirection + new Vector3(0.30f, 0f, 0f);
                mDirection2 = mDirection + new Vector3(0.15f, 0f, 0f);
                mDirection4 = mDirection + new Vector3(-0.15f, 0f, 0f);
                mDirection5 = mDirection + new Vector3(-0.30f, 0f, 0f);
                //Debug.Log("vertical");
            } 
            if (mDirection.x != 0f && mDirection.y == 0f)
            {
                mDirection1 = mDirection + new Vector3(0f, 0.30f, 0f);
                mDirection2 = mDirection + new Vector3(0f, 0.15f, 0f);
                mDirection4 = mDirection + new Vector3(0f, -0.15f, 0f);
                mDirection5 = mDirection + new Vector3(0f, -0.30f, 0f);
                //Debug.Log("horizontal");
            }
        }
    }

    private void Update()
    {
        if (spread1 != null)
        {
            spread1.transform.position += speed * Time.deltaTime * mDirection1;
            spread2.transform.position += speed * Time.deltaTime * mDirection2;
            spread3.transform.position += speed * Time.deltaTime * mDirection;
            spread4.transform.position += speed * Time.deltaTime * mDirection4;
            spread5.transform.position += speed * Time.deltaTime * mDirection5;
        }
        else
        {
            transform.position += speed * Time.deltaTime * mDirection;
        }

        mTimer += Time.deltaTime;
        if (mTimer > timeToDestroy)
        {
            // mTimer = 0f;
            /*
            if (Input.GetButtonUp("Fire1"))
            {
                GameManager.GetInstance().player.SetCooldown(0f);
            }
            */
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hit"))
        {
            Destroy(gameObject);
        }
    }
}
