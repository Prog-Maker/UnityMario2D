using Assets.Mario.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMove : MonoBehaviour
{
    private Rigidbody2D rbody2d;

    public float speed = 3.5f;

    public int HorizontalOnDefault = 1;

    [Header("Layers for find direcrtion")]
    [SerializeField]
    private LayerMask collisionMask;

    [Header("Layers for player")]
    [SerializeField]
    private LayerMask playerMask;

    [Header("Layers for Ignoring")]
    [SerializeField]
    private LayerMask ignorerMask;

    [SerializeField] private Transform groundChecker; // точка обнаружения земли
    [SerializeField] private Transform rigthCheck; // обнаружение препятствия справа
    [SerializeField] private Transform leftCheck; // обнаружение препятствия слева

    private bool OnGround = true;

    private bool MoveRight = true;

    private bool isRayCasting = true;

    [SerializeField]
    private bool needRiacast = false;

    private float rayCastHeigth = 1.5f;

    //private Collider2D meColl;

    void Awake()
    {
        rbody2d = GetComponent<Rigidbody2D>();
        if (groundChecker == null) groundChecker = transform.Find("GroundChecker");
        if (rigthCheck == null) rigthCheck = transform.Find("RightCheck");
        if (leftCheck == null) leftCheck = transform.Find("LeftCheck");

        if (HorizontalOnDefault == -1) MoveRight = false;

       // meColl = GetComponent<Collider2D>();

    }

    private void Update()
    {
        GroundCheck();
        Raycasting();
       // Debug.Log(meColl);
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (OnGround && !rbody2d.isKinematic) rbody2d.velocity = new Vector2(HorizontalOnDefault * speed, 0);
    }

    private void Raycasting()
    {
        if (isRayCasting && needRiacast)
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
        var res = Physics2D.OverlapCircleNonAlloc(groundChecker.position, 0.2f, _colliders, collisionMask);

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

    public bool RigthCheck()
    {
        //if (MoveRight)
        //{
            Collider2D[] _colliders = new Collider2D[5];
            var res = Physics2D.OverlapCircleNonAlloc(rigthCheck.position, 0.12f, _colliders, collisionMask);

            if (res > 1 )
            {
                HorizontalOnDefault *= -1;
                MoveRight = false;
                return true;
            }
        //}

        return false;
    }

    public bool LeftCheck()
    {
        //if (!MoveRight)
        //{
            //Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer);

            Collider2D[] _colliders = new Collider2D[5];
            var res = Physics2D.OverlapCircleNonAlloc(leftCheck.position, 0.12f, _colliders, collisionMask);

            if (res > 1)
            {
                HorizontalOnDefault *= -1;
                MoveRight = true;
                return true;
            }
        //}

        return false;
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(leftCheck.position, 0.12f);
    //    Gizmos.DrawWireSphere(rigthCheck.position, 0.12f);
    //}

    private void OnCollisionStay2D(Collision2D collision)
    {
        RigthCheck();
        LeftCheck();
    }


    //private void OnBecameInvisible()
    //{
    //   // Destroy(gameObject);
    //}

}
