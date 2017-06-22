using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckTwoColliders : Duck {

    private CircleCollider2D circ;
    private CapsuleCollider2D caps;

    protected override void Awake ()
    {
        base.Awake ();

        circ = GetComponent<CircleCollider2D> ();
        caps = GetComponent<CapsuleCollider2D> ();
    }

    protected override void OnDuck (bool value)
    {
        ducking = value;

        if (ducking && caps.enabled)
        {
            circ.enabled = true;
            caps.enabled = !circ.enabled;
        }
        else
        {
            circ.enabled = false;
            caps.enabled = !circ.enabled;
        }
    }
}
