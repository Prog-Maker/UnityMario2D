using Assets.Mario.Scripts;
using MarioWorldForAll;
using System.Collections;
using UnityEngine;

public class GoomlaDie : MonoBehaviour
{
    private Animator _animator;

    private Rigidbody2D rbody;

    private bool Die = false;

    private void Awake()
    {
        _animator = GetComponentInParent<Animator>();

        rbody = GetComponentInParent<Rigidbody2D>();

        //parent = GetComponentInParent<Goomla>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(Tags.Player))
        {
            //parent.isRayCasting = false;

            StartCoroutine(Animate(other.collider));
        }
    }

    private IEnumerator Animate(Collider2D other)
    {
        
        if (!Die)
        {
                Die = true;

                _animator.SetBool("Die", Die);

               // other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 13.5f, ForceMode2D.Impulse);

                GameController.instance.PlaySound("smb_stomp");

                rbody.constraints = RigidbodyConstraints2D.FreezePosition;

                GetComponentInParent<CapsuleCollider2D>().enabled = false;

                yield return null;//new WaitForSeconds(1f);

                Destroy(transform.parent.gameObject, 0.1f);

        }
    }

}
