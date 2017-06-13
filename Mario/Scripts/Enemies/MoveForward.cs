using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private enum Direction
    {
        right =1, left =-1
    }

    public float speed;

    [SerializeField]
    private Direction direction;

    private Rigidbody2D rbody;

	void Awake ()
    {
        rbody = GetComponent<Rigidbody2D> ();
	}

    private void Start ()
    {
        var x = transform.localScale.x;
        transform.localScale = new Vector3 ((float) direction * x, transform.localScale.y, transform.localScale.z);
    }

    void FixedUpdate ()
    {
        rbody.velocity = new Vector2 (transform.localScale.x * speed, rbody.velocity.y) ;
	}
}


