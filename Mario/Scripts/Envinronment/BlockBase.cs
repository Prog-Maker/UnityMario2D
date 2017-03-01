using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBase : MonoBehaviour
{
    private float verticalDistanceMove = 0.24f;

    private bool up = true;

    private IEnumerator Move(Rigidbody2D rbody)
    {
        
        yield return new WaitForSeconds(0.05f);

        while (true)
        {

            if (up)
            {
                rbody.position = new Vector2(rbody.position.x, rbody.position.y + verticalDistanceMove);

                up = !up;

                yield return new WaitForSeconds(0.08f);
            }

            if (!up)
            {
                rbody.position = new Vector2(rbody.position.x, rbody.position.y - verticalDistanceMove);

                up = !up;

                yield return new WaitForSeconds(0.08f);

                break;
            }

        }
    }

    public void MoveBlock(Rigidbody2D rbody)
    {
        StartCoroutine(Move(rbody));
    }


    //private void OnCollisionStay2D(Collision2D other)
    //{
    //    var rbody2d = other.gameObject.GetComponent<Rigidbody2D>();

    //    if (!up && rbody2d && other.gameObject.CompareTag(Tags.Enemy))
    //    {
    //        //Debug.Log(other.gameObject.name);
    //        rbody2d.AddForce(new Vector2(/*Random.Range(-0.5f, 0.5f)*/0, 1 * 4.5f), ForceMode2D.Impulse);
    //    }
    //}
}
