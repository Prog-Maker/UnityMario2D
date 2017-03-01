using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(BillPlayer2D))]
public class BillMoveController : MonoBehaviour
{

    private BillPlayer2D Character;
    private bool Jump;
    private Rigidbody2D rbody2d;


    private void Awake()
    {
        Character = GetComponent<BillPlayer2D>();
        rbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!Jump)
        {
            // Если не в прыжке считываем кнопку прыжок
            Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }

        if (!rbody2d)  Move();
    }

    void FixedUpdate ()
    {
        if (rbody2d != null)
        {
            Move();
        }
    }

    private void Move()
    {
        // считываем импуты ))
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        float h = CrossPlatformInputManager.GetAxis("Horizontal");

        // Передаем параметры в BillPlayer2D
        Character.Move(h, v, Jump);
        Jump = false;
    }
}
