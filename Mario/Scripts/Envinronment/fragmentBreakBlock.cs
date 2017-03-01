using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fragmentBreakBlock : MonoBehaviour {


    [SerializeField] private float powerForce;
    [SerializeField] private Vector2 direction;
    private Rigidbody2D rbody;

	void Start ()
    {
        rbody = GetComponent<Rigidbody2D>();
        
        Invoke("AddForse", 0f);
    }
	
	// Update is called once per frame
	void AddForse()
    {
        rbody.isKinematic = false;
        rbody.AddForce(direction * powerForce, ForceMode2D.Impulse);
	}
}
