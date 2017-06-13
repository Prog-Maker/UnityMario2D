using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvertPlatform : MonoBehaviour
{
    public float invertTime;

    private bool inVert = false;

    private JumpDown jumpdown;
    

    void onInvert ()
    {
        transform.localScale = new Vector3 (1, 1, 1);
        inVert = false;
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag ("Player"))
        {
            jumpdown = collision.gameObject.GetComponent<JumpDown> ();
            inVert = true;
        }
    }

    private void OnCollisionStay2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag ("Player"))
        {
            
            if (jumpdown.onJumpDown && inVert)
            {
                transform.localScale = new Vector3 (1, -1, 1);

                //jumpdown.onJumpDown = false;

                Invoke ("onInvert", invertTime);
            }
        }
    }

    private void OnCollisionExit2D (Collision2D collision)
    {
        if (collision.gameObject.CompareTag ("Player"))
        {
            inVert = false;
        }
    }
}
