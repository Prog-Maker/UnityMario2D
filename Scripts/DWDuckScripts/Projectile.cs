using UnityEngine;


namespace ForGlobal
{
    public class Projectile : MonoBehaviour
    {

        public float speed = 150;

        public GameObject ExplodeEffectPrefab;

        private Rigidbody2D body2d;


        private void Awake ()
        {
            body2d = GetComponent<Rigidbody2D> ();
        }


        private void Start ()
        {
            var startVelX = speed * transform.localScale.x;

            body2d.velocity = new Vector2 (startVelX, body2d.velocity.y);

            Destroy (gameObject, 3.0f);
        }


        private void OnTriggerEnter2D (Collider2D collision)
        {
            Animate ();
            Destroy (gameObject);
        }


        private void Animate ()
        {
            if (ExplodeEffectPrefab)
            {
                Instantiate (ExplodeEffectPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}