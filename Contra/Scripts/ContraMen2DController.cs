using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(ContraMen2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ContraMen2DController : MonoBehaviour {

    private ContraMen2D contraMen;

    private bool Jump;

    private bool Shoot;

    private float speedUp;

    void Start()
    {
        contraMen = GetComponent<ContraMen2D>();
    }


    void Update()
    {
        if (!Jump)
        {
            Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }

        if (!Shoot)
        {
            Shoot = CrossPlatformInputManager.GetButton("Fire1");
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedUp *= 1.5f;
        }
    }


    private void FixedUpdate()
    {
        // считываем импуты ))
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        float h = CrossPlatformInputManager.GetAxis("Horizontal");

        h = h * speedUp;

        // Передаем параметры в MarioCharacter2D
        contraMen.Move(h, v, Jump);
        contraMen.Shoot(Shoot);


        Jump = false;
        Shoot = false;

        speedUp = 1;
    }
}
