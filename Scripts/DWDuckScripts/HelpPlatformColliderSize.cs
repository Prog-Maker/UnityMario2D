using UnityEngine;

public class HelpPlatformColliderSize : MonoBehaviour
{

    private BoxCollider2D colliderMy;
    public  BoxCollider2D mainCollider;

    public Vector2 newSize;
    public Vector2 offSet;

    void Awake ()
    {
        colliderMy = GetComponent<BoxCollider2D> ();
    }


    void Start ()
    {
        newSize = new Vector2 (mainCollider.size.x, colliderMy.size.y);
        offSet = new Vector2 (mainCollider.offset.x, colliderMy.offset.y);

        colliderMy.size = newSize;
        colliderMy.offset = offSet;
    }


}
