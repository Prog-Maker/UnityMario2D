using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour {

    public float speed;

    public float liveTime = 8f;

    private Rigidbody2D rbody;

    private void Awake ()
    {
        rbody = GetComponent<Rigidbody2D> ();
    }

    private void Start ()
    {
        Destroy (gameObject, liveTime);
    }

    void Update ()
    {
        rbody.velocity = new Vector2 (transform.localScale.x, 0) * speed;
	}
}
