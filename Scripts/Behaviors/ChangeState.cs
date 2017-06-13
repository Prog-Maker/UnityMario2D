using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : AbstractBehavior {

    public bool state = false;

    public void changeState (bool value)
    {
        state = value;
        ToggleScripts (!value);
    }

    
}
