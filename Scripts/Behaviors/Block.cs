
using UnityEngine;

public class Block : AbstractBehavior {


    public bool blocking;


	void Update () {
        var canBlock = inputState.GetButtonValue (inputButtons [0]);
        if (canBlock && collisionState.standing && !blocking)
        {
            OnBlock (true);
        }
        else if (blocking && !canBlock)
        {
            OnBlock (false);
        }
    }

    private void OnBlock (bool value)
    {
        blocking = value;
        ToggleScripts (!blocking);
    }
}
