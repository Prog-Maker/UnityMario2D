using UnityEngine;

public class BillPlayer2D : MonoBehaviour
{

    [SerializeField] private float _maxSpeed = 10f;                    // Скорость (расстояние в пунктах) передвижения игрока за один FixedUpdate
    [SerializeField] private float _jumpForce = 10f;                  // Сила прилагаемая для прыжка

    private bool OnGround = true; // На земле стоим

    private float GroundChekDistance = 0.3f;

    [SerializeField]
    private Transform[] GroundCheckers;

    private Rigidbody2D rbody;
    private Animator[] animators; // Аниматоры
    private SpriteRenderer[] bodys; // Рендеры для ФЛИПА

    bool moving = false; // движемся ли в гор. напрвлении
    bool jumped = false; // в прыжке ли
    bool KeyDown = false; // клавиша вниз нажата ли
    int direction; // Куда смотрим

    bool InWater = false;


    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        animators = GetComponentsInChildren<Animator>();
        bodys = GetComponentsInChildren<SpriteRenderer>();
    }


    private void Update()
    {
        CheckGround();
        Animate();

        Debug.Log(OnGround);
    }

    private void CheckGround()
    {
        for (int i = 0; i < GroundCheckers.Length; i++)
        {

            RaycastHit2D hit = Physics2D.Raycast(GroundCheckers[i].position, -Vector2.up, GroundChekDistance);

            Debug.DrawLine(GroundCheckers[i].position, new Vector2(GroundCheckers[i].position.x, GroundCheckers[i].position.y - GroundChekDistance), Color.red);

            if (hit.collider != null && hit.collider.gameObject.CompareTag("Ground"))
            {
                OnGround = true;
                jumped = false;
                InWater = false;
                break;
            }
            else if (hit.collider != null && hit.collider.gameObject.CompareTag("Water"))
            {
                InWater = true;
                break;
            }
            else
            {
                jumped = true;
                OnGround = false;
            }
        }
    }


    public void Move(float horDirection, float verDirection, bool jump)
    {
        moving = Moving(horDirection);
        KeyDown = KeyDownPress(verDirection);
        direction = Direction(horDirection, verDirection);

        // движение вперед / назад
        if (OnGround || InWater)
        {
            if (moving)
            {
                for (int i = 0; i < bodys.Length; i++)
                {
                    bodys[i].flipX = Flip(horDirection);
                }
            }

            if (rbody != null)
            {
                rbody.velocity = new Vector2(horDirection * _maxSpeed, rbody.velocity.y);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, Vector2.right * horDirection, _maxSpeed * Time.deltaTime);
            }
        }

        // Прыжок
        if (OnGround && jump)
        {

            // Добавляем вертикальную силу для прыжка
            if (verDirection == 0) verDirection += 1;

            if (rbody != null)
            {
                rbody.AddForce(new Vector2(0f, verDirection), ForceMode2D.Impulse);
            }
            // rbody.velocity = Vector2. (-5f, verDirection * _jumpForce);
            // rbody.velocity = new Vector2(0f, rbody.velocity.y);
        }

        if (OnGround && !jump && verDirection < 0)
        {
            //animators.SetTrigger("Lie");
        }

    }


    int Direction(float hdir, float vdir)
    {
        if (hdir == -1 && vdir == 1) return 7;      // Влево вверх
        if (hdir == 1 && vdir == 1) return 9;       // Вправо вверх

        if (hdir == 1 && vdir == -1) return 3;      // Впарво вниз
        if (hdir == -1 && vdir == -1) return 1;     // Влево вниз

        if (jumped && vdir == -1) return 2;
        if (hdir == 0 && vdir == 1) return 8;         //Вверх

        return 0;
    }

    bool KeyDownPress(float vDirection)
    {
        if (vDirection < 0) return true;
        return false;
    }

    bool Moving(float hDirection)
    {
        if (hDirection != 0) return true;
        return false;
    }

    bool Flip(float direction)
    {
        if (direction < 0) return true;
        return false;
    }

    // Анимация
    private void Animate()
    {
        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].SetBool("OnGround", OnGround);
            //   animators[i].SetBool("Jumped", jumped);
            animators[i].SetBool("Moving", moving);
            //   animators[i].SetFloat("vSpeed", rbody.velocity.y);
            /* animators[i].SetBool("Shooting", KeyAction);*/
            //  animators[i].SetBool("KeyDown", KeyDown);
            // animators[i].SetInteger("Direction", direction);
            animators[i].SetBool("InWater", InWater);
        }
    }

}

public class Tags : MonoBehaviour
{
    public static string Ground = "Ground";
    public static string MarioHead = "MarioHead";
    public static string Player = "Player";
    public static string FunGusPower = "FunGusPower";
    public static string FunGusLive = "FunGusLive";

    public static string Enemy = "Enemy";
}

