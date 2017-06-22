using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateState : AbstractBehavior {

    public ActivationBehavior behaviorToActivate;

	void Update ()
    {
        var canActivate = inputState.GetButtonValue(inputButtons[0]);

        if (canActivate && !behaviorToActivate.isActivated)
        {
            behaviorToActivate.isActivated = true;
        }
	}
}
