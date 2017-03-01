using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInMoment : MonoBehaviour {

    [SerializeField] private bool IsFreeze = false;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Start()
    {
        if (IsFreeze)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }

    private void OnBecameVisible()
    {
        if (IsFreeze)
        {
           rb.constraints = RigidbodyConstraints2D.None;
           rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

    }

}
