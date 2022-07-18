using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform followAt;

    private void Update()
    {
        if (followAt.position.x > 0)
        {
            transform.position = new Vector3(
                followAt.position.x,
                transform.position.y,
                transform.position.z
            );
        }
    }
}