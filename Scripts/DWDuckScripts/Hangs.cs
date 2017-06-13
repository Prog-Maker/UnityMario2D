using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hangs : AbstractBehavior {

	
	void FixedUpdate ()
    {
		if (collisionState.onRoof)
        {
            ToggleScripts (false);
        }
        else
        {
            ToggleScripts (true);
        }
	}
}
