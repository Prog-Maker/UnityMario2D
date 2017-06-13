using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour {


    public Camera camForOn;
    public Camera camForOff;

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag ("Player"))
        {
            camForOn.enabled = true;
            camForOff.enabled = false;
        }
    }
}
