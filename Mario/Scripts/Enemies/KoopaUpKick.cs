using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaUpKick : MonoBehaviour {


    private Koopa parent;

    private void Awake()
    {
        parent = GetComponentInParent<Koopa>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        parent.OnKickUp(collision.gameObject);
    }

   
}
