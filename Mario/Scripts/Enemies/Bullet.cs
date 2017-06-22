using MarioWorldForAll;
using UnityEngine;


public class Bullet : MonoBehaviour {

    [SerializeField]
    private float speed = 12.5f;

   
    protected Rigidbody2D rbody;

    protected Animator _anim;

    protected virtual void Awake ()
    {
        rbody = GetComponent<Rigidbody2D> ();
        _anim = GetComponent<Animator> ();
    }

    protected virtual void Start ()
    {
        rbody.velocity = new Vector2 (transform.localScale.x, 0) * speed;
    }

    protected virtual void OnTriggerEnter2D (Collider2D target)
    {
        if (target.gameObject.CompareTag ("Enemy"))
        {
            var enemy = target.GetComponent<Enemy>();

            if (enemy)
            {
               // Destroy (gameObject);
                _anim.SetInteger ("AnimState", 1);
                enemy.Kill ();
            }
        }
    }


    protected virtual void OnCollisionEnter2D (Collision2D target)
    {
        if (target.gameObject.CompareTag ("Enemy"))
        {
            var enemy = target.gameObject.GetComponent<Enemy>();

            if (enemy)
            {
                //Destroy (gameObject);
                _anim.SetInteger ("AnimState", 1);
                enemy.Kill ();
            }
        }
    }
}
