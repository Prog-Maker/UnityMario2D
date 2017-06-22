using System;
using UnityEngine;

namespace MarioWorldForAll
{

    public class Enemy : MonoBehaviour
    {
        [SerializeField] private LayerMask bulletLayer;

        protected Animator _anim;
        protected Rigidbody2D rbody2d;

        protected virtual void Awake ()
        {
            _anim = GetComponent<Animator> ();
            rbody2d = GetComponent<Rigidbody2D> ();
        }


        int count = 0;
        private void OnTriggerEnter2D(Collider2D other)
        {
            count += 1;

            if (count == 1)
            {
                if (1 << other.gameObject.layer == bulletLayer)
                {
                    Destroy(other);
                    Kill();
                    count ++;
                    return;
                }
            }
            count = 0;

        }

        public virtual void Kill()
        {
            Debug.Log("Kill");
        }
    }
}