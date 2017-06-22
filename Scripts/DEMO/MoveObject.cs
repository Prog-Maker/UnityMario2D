using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour {

    public Vector3 A; // = new Vector3(x,y,z); // С какой точки двигаться
    public float b; // = distance; // На какое расстояние нужно переместиться
    
    public Vector3 C;// = direction; // Вектор направления

    public Vector3 B;

    void Start ()
    {
        transform.position = A;
	}
	
	
	void Update ()
    {
		if (Input.GetKeyDown (KeyCode.W))
        {
            B = C * b + A;
            transform.position = B;
        }

        Debug.DrawLine (A, B);
	}
}
