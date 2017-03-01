using MarioWorldForAll;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


[RequireComponent(typeof(MarioCharacter2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class MarioController : MonoBehaviour
{
    private MarioCharacter2D mario;

    private bool Jump;
   // private bool JumpLong;

    private float speedUp;

    void Start ()
    {
        mario = GetComponent<MarioCharacter2D>();
    }
	
	
	void Update ()
    {
        if (!Jump)
        {
            Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            //JumpLong = CrossPlatformInputManager. GetButton("Jump");
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedUp *= 1.5f;
        }

       // Debug.Log(string.Format("Jump - {0} , JumpLong - {1}", Jump, JumpLong));
        
    }


    private void FixedUpdate()
    {
        // считываем импуты ))
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        float h = CrossPlatformInputManager.GetAxis("Horizontal");

        h = h * speedUp;

        // Передаем параметры в MarioCharacter2D
        mario.Move(h, v, Jump);

        Jump = false;
        speedUp = 1;
    }
}
