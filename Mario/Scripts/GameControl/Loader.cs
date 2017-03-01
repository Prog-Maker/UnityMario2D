using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

    public GameObject gameController;
	
	void Start ()
    {
        if (GameController.instance == null) Instantiate(gameController);
	}
	
}
