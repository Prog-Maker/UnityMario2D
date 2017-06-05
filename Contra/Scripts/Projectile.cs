using UnityEngine;

public class Projectile : MonoBehaviour {

    Rigidbody2D myRigidbody;
    public float movespeed;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myRigidbody.AddForce(transform.right * movespeed, ForceMode2D.Impulse);
	}
	
	void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
