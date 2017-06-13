using UnityEngine;
using System.Collections;

public class CollisionState : MonoBehaviour
{

    public LayerMask collisionLayer;
    public bool standing;
    public bool onWall;
    public bool onRoof;

    public bool needWallCollision = false;
    public bool needTopCollision = false;

    public Vector2 topPositon = Vector2.zero;

    public Vector2 bottomPosition = Vector2.zero;

    public Vector2 rightPosition = Vector2.zero;
    public Vector2 leftPosition = Vector2.zero;

    public float collisionRadius = 10f;
    public Color debugCollisionColor = Color.red;

    private InputState inputState;

    void Awake ()
    {
        inputState = GetComponent<InputState> ();
    }


    void FixedUpdate ()
    {

        var pos = bottomPosition;
        pos.x += transform.position.x;
        pos.y += transform.position.y;

        standing = Physics2D.OverlapCircle (pos, collisionRadius, collisionLayer);

        // Debug.Log (standing);

        if (needTopCollision)
        {
            pos = topPositon;
            pos.x += transform.position.x;
            pos.y += transform.position.y;

            onRoof = Physics2D.OverlapCircle (pos, collisionRadius, collisionLayer);
        }


        if (needWallCollision)
        {
            pos = inputState.direction == Directions.Right ? rightPosition : leftPosition;
            pos.x += transform.position.x;
            pos.y += transform.position.y;

            onWall = Physics2D.OverlapCircle (pos, collisionRadius, collisionLayer);
        }
        else onWall = false;
    }


    void OnDrawGizmos ()
    {
        Gizmos.color = debugCollisionColor;

        Vector2[] positions;

        if (needTopCollision && needWallCollision)
        {
            positions = new Vector2 [] { rightPosition, bottomPosition, leftPosition, topPositon};
        }
        else if (needWallCollision && !needTopCollision)
        {
            positions = new Vector2 [] { rightPosition, bottomPosition, leftPosition };
        }
        else if (!needWallCollision && needTopCollision)
        {
            positions = new Vector2 [] { bottomPosition, topPositon};
        }
        else
        {
            positions = new Vector2 [] { bottomPosition };
        }


        foreach (var position in positions)
        {
            var pos = position;
            pos.x += transform.position.x;
            pos.y += transform.position.y;

            Gizmos.DrawWireSphere (pos, collisionRadius);
        }
    }
}
