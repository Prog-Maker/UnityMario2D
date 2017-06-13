using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForward : MonoBehaviour {

    [SerializeField] private Transform sightStart, sightEnd;
    [SerializeField] private bool needsCollision;
    [SerializeField] private LayerMask layer;

    protected float x;

    protected float curPos;

    protected virtual void Start ()
    {
        x = transform.localScale.x;
        curPos = transform.position.x;
    }

    protected virtual void Update ()
    {
        var collision = Physics2D.Linecast (sightStart.position, sightEnd.position, layer);

        Debug.DrawLine (sightStart.position, sightEnd.position, Color.green);

        if (collision == needsCollision)
        {
            transform.localScale = new Vector3 (transform.localScale.x == x ? -1 * x : x, transform.localScale.y, transform.localScale.z);
            curPos = transform.position.x;
        }

	}
}
