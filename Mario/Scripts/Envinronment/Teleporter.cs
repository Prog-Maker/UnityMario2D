using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform teleportPoint, endPoint;

    public float teleportPointSize;
    public float endtPointSize;

    public LayerMask layerForNonPlayerColliders;

    private bool onTeleport = false;

    public  List<MonoBehaviour> scripts;

    public List<Collider2D> colliders;


    private void Update ()
    {
        CheckCollisionOnTeleportPoint ();

        if (endPoint.gameObject.activeSelf)
        {
            CheckCollisionOnEndPoint ();
        }
    }

    private void OnTriggerStay2D (Collider2D other)
    {

        if (other.gameObject.CompareTag ("Player") && Input.GetAxis ("Vertical") < 0 && !onTeleport)
        {
            scripts = new List<MonoBehaviour> ();

            var walk = other.GetComponent<Walk>();
            var duck = other.GetComponent<Duck>();
            var jump = other.GetComponent<Jump>();


            if (walk)
            {
                walk.enabled = false;
                scripts.Add (walk);
            }
            if (duck)
            {
                if (duck.enabled)
                {
                    duck.enabled = false;
                    scripts.Add (duck);
                }
            }
            if (jump)
            {
                jump.enabled = false;
                scripts.Add (jump);
            }

            FindColliders ();

            foreach (var col in colliders)
            {
                col.enabled = false;
            }

            onTeleport = true;

        }


    }


    private void OnTeleport (GameObject player)
    {
        endPoint.gameObject.SetActive (true);

        player.transform.position = endPoint.position;

        foreach (var col in colliders)
        {
            col.enabled = true;
        }

        //colliders.Clear ();
    }


    private void CheckCollisionOnTeleportPoint ()
    {
        var player = Physics2D.OverlapCircle(teleportPoint.position, teleportPointSize);

        if (player && player.gameObject.CompareTag ("Player"))
        {
            OnTeleport (player.gameObject);
        }
    }

    private void CheckCollisionOnEndPoint ()
    {
        var player = Physics2D.OverlapCircle(endPoint.position, endtPointSize);

        if (player)
        {
            foreach (var s in scripts)
            {
                s.enabled = true;
            }

            scripts.Clear ();
            onTeleport = false;
            endPoint.gameObject.SetActive (false);
        }

    }

    private Collider2D coll = null;

    private void FindColliders ()
    {

        if (coll == null)
        {
            coll = Physics2D.OverlapCircle (transform.position, teleportPointSize, layerForNonPlayerColliders);
        }


        if (colliders.Count < coll.gameObject.GetComponents<BoxCollider2D> ().Length)
        {
            foreach (var c in coll.gameObject.GetComponents<BoxCollider2D> ())
            {
                colliders.Add (c);
            }
        }
    }


    private void OnDrawGizmos ()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere (teleportPoint.position, teleportPointSize);

        Gizmos.DrawWireSphere (endPoint.position, endtPointSize);

        Gizmos.DrawWireSphere (transform.position, teleportPointSize);

        Gizmos.color = Color.blue;

        Gizmos.DrawLine (transform.position, endPoint.position);

        Gizmos.color = Color.green;

        Gizmos.DrawLine (transform.position, teleportPoint.position);
    }
}
