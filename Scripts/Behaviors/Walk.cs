using UnityEngine;

public class Walk : AbstractBehavior
{

    public float speed = 50f;
    public float runMultiplier = 2f;
    public bool running;


    void FixedUpdate ()
    {

        running = false;

        var right = inputState.GetButtonValue (inputButtons [0]);
        var left = inputState.GetButtonValue (inputButtons [1]);

        bool run = false;

        if (inputButtons.Length > 2)
        {
            run = inputState.GetButtonValue (inputButtons [2]);
        }

        if (right || left)
        {
            var tmpSpeed = speed;

            if (run && runMultiplier > 0)
            {
                tmpSpeed *= runMultiplier;
                running = true;
            }

            var velX = tmpSpeed * (float)inputState.direction;

            body2d.velocity = new Vector2 (velX, body2d.velocity.y);
        }
        else
        {
            body2d.velocity = new Vector2 (0, body2d.velocity.y);
        }

        //Debug.Log ("right - " + right);
        //Debug.Log ("left - " + left);
    }
}
