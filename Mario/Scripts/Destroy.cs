using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

    [SerializeField]private float timeWait = 0;

    void Start ()
    {
        Invoke("DestroyI", timeWait);
	}

    private void DestroyI()
    {
        Destroy(gameObject);
    }
}
