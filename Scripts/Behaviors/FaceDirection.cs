using UnityEngine;

public class FaceDirection : AbstractBehavior
{
    private float x;

    private void Start ()
    {
        x = transform.localScale.x;
    }


    private void Update ()
    {
		var right = inputState.GetButtonValue (inputButtons [0]);
		var left = inputState.GetButtonValue (inputButtons [1]);

		if (right)
        {
			inputState.direction = Directions.Right;
		}
        else if (left)
        {
			inputState.direction = Directions.Left;
		}

        transform.localScale = new Vector3 ((float)inputState.direction * x, transform.localScale.y, transform.localScale.z);
	}
}
