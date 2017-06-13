using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDown : AbstractBehavior
{
    public bool onJumpDown;

    void Update ()
    {
        onJumpDown = false;
        ToggleScripts (true);

        var canDown = inputState.GetButtonValue(inputButtons[0]);  
        var canJump = inputState.GetButtonValue(inputButtons[1]);

        if (canDown && canJump)
        {
            onJumpDown = true;
        }

        if (onJumpDown)
        {
            ToggleScripts (false);
        }
    }

}
