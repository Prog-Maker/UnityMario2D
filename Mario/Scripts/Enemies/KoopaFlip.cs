using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoopaFlip : MonoBehaviour {


    [SerializeField]
    private SpriteRenderer[] spriteToFlip;

    //[SerializeField] private Transform damagePoint;
    //[SerializeField] private float damageRadius = 1f;

    private Rigidbody2D rbody2d;

    private bool NeedFlip = true;

    private void Awake()
    {
        rbody2d = GetComponentInParent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
         CheckDirection();
    }

    private void CheckDirection()
    {
        if (rbody2d.velocity.x > 0)
        {
            for (int i = 0; i < spriteToFlip.Length; i++)
            {
                if  (!spriteToFlip[i].flipX) spriteToFlip[i].flipX = true;
            }
        }
        else if (rbody2d.velocity.x < 0)
        {
            for (int i = 0; i < spriteToFlip.Length; i++)
            {
                if (spriteToFlip[i].flipX) spriteToFlip[i].flipX = false;
            }
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(damagePoint.position, damageRadius);
    //    Debug.Log("Collisisn");
    //}

}
