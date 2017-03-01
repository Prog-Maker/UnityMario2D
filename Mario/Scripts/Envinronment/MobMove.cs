using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMove : MonoBehaviour {

    private Rigidbody2D rbody2d;



    public float speed = 3.5f;

    [SerializeField] private int HorizontalOnDefault = 1;

    [Header("Layers for find direcrtion")]
    [SerializeField] private LayerMask collisionMask;

    [Header("Layers for player")]
    [SerializeField] private LayerMask playerMask;

    [SerializeField] private Transform groundChecker; // точка обнаружения земли
    [SerializeField] private Transform rigthCheck; // обнаружение препятствия справа
    [SerializeField] private Transform leftCheck; // обнаружение препятствия слева

    private bool OnGround = true;

    [SerializeField] private bool MoveRight = true;

    [HideInInspector]
    public bool isRayCasting = true;

    private float rayCastHeigth = 1.5f;

    void Awake()
    {
        rbody2d = GetComponent<Rigidbody2D>();
        if (groundChecker == null) groundChecker = transform.FindChild("GroundChecker");
        if (rigthCheck == null) rigthCheck = transform.FindChild("RightCheck");
        if (leftCheck == null) leftCheck = transform.FindChild("LeftCheck");

        if (HorizontalOnDefault == -1) MoveRight = false;
    }

    private void Update()
    {
        GroundCheck();
        Raycasting();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (OnGround && !rbody2d.isKinematic ) rbody2d.velocity = new Vector2(HorizontalOnDefault * speed, 0);
    }

    private void Raycasting()
    {

       if (isRayCasting)
        {
            RaycastHit2D hit = Physics2D.Raycast(groundChecker.position, Vector2.down, rayCastHeigth, playerMask);
            Debug.DrawLine(groundChecker.position, new Vector2(groundChecker.position.x, groundChecker.position.y - rayCastHeigth), Color.green);

            if (hit.collider != null)
            {
                GetComponent<BaseMoveMob>().OnRayCastEnter();
                isRayCasting = false;
            }
        }
    }

    private void GroundCheck()
    {
        Collider2D[] _colliders = new Collider2D[25];
        var res = Physics2D.OverlapCircleNonAlloc(groundChecker.position, 0.2f, _colliders);

        if (res > 1)
        {
            foreach (var item in _colliders)
            {
                if (item != null && item.gameObject.CompareTag(Tags.Ground))
                {
                    OnGround = true;
                    isRayCasting = true;
                    return;
                }
            }
        }
        OnGround = false;
        isRayCasting = false;
    }

    private void RigthCheck()
    {
        if (MoveRight)
        {
            Collider2D[] _colliders = new Collider2D[5];
            var res = Physics2D.OverlapCircleNonAlloc(rigthCheck.position, 0.12f, _colliders, collisionMask);

            if (res >= 1)
            {
                HorizontalOnDefault *= -1;
                MoveRight = false;
                return;
            }
        }
    }

    private void LeftCheck()
    {
        if (!MoveRight)
        {
            //List<Collider2D> _colliders = new List<Collider2D>();
            Collider2D[] _colliders = new Collider2D[5];
            var res = Physics2D.OverlapCircleNonAlloc(leftCheck.position, 0.12f, _colliders, collisionMask);
            //print(Physics2D.OverlapCircleAll(leftCheck.position, 0.12f, collisionMask));

           // var res = Physics2D.OverlapCircleAll(leftCheck.position, 0.12f, collisionMask);
            if (res >= 1)
            {
                HorizontalOnDefault *= -1;
                MoveRight = true;
                return;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(leftCheck.position, 0.12f);
        Gizmos.DrawWireSphere(rigthCheck.position, 0.12f);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        RigthCheck();
        LeftCheck();
    }
    //private void OnBecameInvisible()
    //{
    //   // Destroy(gameObject);
    //}

}
