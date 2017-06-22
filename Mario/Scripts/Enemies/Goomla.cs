using System;
using UnityEngine;

namespace MarioWorldForAll
{
    public class Goomla : Enemy
    {

        [SerializeField] private float powerImpulse;

        //public override void OnRayCastEnter()
        //{
        //    Die();
        //}
        bool die = false;

        private void Update ()
        {
            if (rbody2d.velocity.y > 60 && !die) { Die (); }
        }


        private void OnCollisionEnter2D (Collision2D other)
        {
            // Debug.Log(other.relativeVelocity.magnitude);
            //if (other.relativeVelocity.magnitude > 20) Die ();

            if (other.collider.tag == "Player")
            {
                foreach (ContactPoint2D point in other.contacts)
                {

                    print (point.normal);
                    if (point.normal.y < 0.3)
                    {
                        print ("Coll in Top");
                    }
                    else if ((point.normal.y > 0.3))
                    {
                        print ("Coll in Bottom");
                    }
                    else
                    {
                        print ("Coll in side");
                    }
                }
            }
        }


        private void Die ()
        {
            die = true;
            transform.SetLocalScaleY (-transform.localScale.y);
            GetComponent<Collider2D> ().enabled = false;

            _anim.enabled = false;

            rbody2d.constraints = RigidbodyConstraints2D.None;

            rbody2d.AddForce (new Vector2 (0, 1 * powerImpulse), ForceMode2D.Impulse);
            //GameController.instance.PlaySound("smb_stomp");

            rbody2d.gravityScale = 15;
            Destroy (gameObject, 1.5f);
        }

        public override void Kill ()
        {
            Die ();
        }

        /*private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }*/

    }
}