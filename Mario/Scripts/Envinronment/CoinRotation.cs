using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRotation : MonoBehaviour {

    private Rigidbody2D rbody;

    public float FlySpeed = 100.5f;

    private void Awake ()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Move();
	}

    private void Move()
    {
       // GameController.instance.PlaySound("smb_coin");
        rbody.velocity = new Vector2(rbody.velocity.x, rbody.velocity.y + FlySpeed);
    }

    void Update ()
    {
        if (rbody.velocity.y < -50)
        {
            Destroy(gameObject);
        }

        if (rbody.velocity.y > 0)
        {
            rbody.gravityScale = 6;
        }
    }
}
