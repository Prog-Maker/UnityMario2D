using UnityEngine;
using System.Collections;

public class FireBall : Bullet
{

    public Vector2 initialVelocity = new Vector2(100, -100);
    public int bounces = 3;

    

    //protected override void Awake ()
    //{
    //    base.Awake ();
        
    //}

    // Use this for initialization
    protected override void Start ()
    {
        var startVelX = initialVelocity.x * transform.localScale.x;

        rbody.velocity = new Vector2 (startVelX, initialVelocity.y);
    }

    protected override void OnCollisionEnter2D (Collision2D target)
    {
        if (target.collider.tag == "Wall")
        {
            _anim.SetInteger ("AnimState", 1);
        }

        if (target.gameObject.transform.position.y < transform.position.y)
        {
            bounces--;
        }

        if (bounces <= 0)  Destroy (gameObject);

        base.OnCollisionEnter2D (target);
    }

    private void OnDestroy ()
    {
        Destroy (gameObject);
    }
}
