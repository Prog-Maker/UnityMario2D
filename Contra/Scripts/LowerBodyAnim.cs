using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerBodyAnim : MonoBehaviour {

    private Animator _animator;

	void Start ()
    {
        _animator = GetComponent<Animator>();
	}
	
	
	void Update ()
    {
        float speed = Input.GetAxisRaw("Horizontal");
       // Debug.Log(speed);
        _animator.SetFloat("FSpeed", Mathf.Abs(speed));
	}
}