using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLookForward : LookForward
{

    public float maxDistance;

    public float currnetDistance = 200f;

    public float moveDistance;

    protected override void Update ()
    {
        base.Update ();

        moveDistance = Mathf.Abs(transform.position.x - curPos);

        if (moveDistance >= currnetDistance)
        {
            transform.localScale = new Vector3 (transform.localScale.x == x ? -1 * x : x, transform.localScale.y, transform.localScale.z);
            currnetDistance = Random.Range (200f, maxDistance);
            moveDistance = 0f;
            curPos = transform.position.x;
        }
    }
}
