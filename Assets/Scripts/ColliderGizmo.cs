using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderGizmo : MonoBehaviour
{
    public BoxCollider2D mCollider;
    public Color gizmoColor = Color.green;

    private void OnDrawGizmos()
    {
        if (mCollider == null)
            return;
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(mCollider.bounds.center, mCollider.bounds.size);
    }
}
