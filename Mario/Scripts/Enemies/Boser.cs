using System;
using System.Collections;
using System.Collections.Generic;
using MarioWorldForAll;
using UnityEngine;

public class Boser : Enemy
{
    [SerializeField]
    private float Health=5f;

    [SerializeField]
    private float damage = 0.5f;


    public override void Kill ()
    {
        Health -= damage;

        if (Health <= 0) Die ();
    }

    private void Die ()
    {
        rbody2d.constraints = RigidbodyConstraints2D.None;

        transform.SetLocalScaleY (-transform.localScale.y);

        var colliders = GetComponents<Collider2D> ();

        foreach (var c in colliders)
        {
            c.enabled = false;
        }

        //_anim.enabled = false;
        rbody2d.gravityScale = 20;
        Destroy (gameObject, 1.5f);

    }
}
