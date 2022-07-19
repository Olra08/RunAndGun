using UnityEngine;

public class ExplosionEnemy : MonoBehaviour
{
    private float mTimer = 0f;

    private void Update()
    {
        mTimer += Time.deltaTime;
        if (mTimer > 0.3f)
        {
            Destroy(gameObject);
        }
    }
}