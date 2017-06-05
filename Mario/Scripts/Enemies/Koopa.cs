using Assets.Mario.Scripts;
using MarioWorldForAll;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : Enemy
{

    //[SerializeField] private LayerMask damagelayer;

    //[SerializeField] private LayerMask ignoreLayer;

    [Tooltip("Сила подбрасывания при уничтожении")]
    [SerializeField] private float powerImpulse;

    [SerializeField] GameObject damagepoint;

    private MobMove moveController;

    private float oldSpeed;

    private Animator _animator;
    private Rigidbody2D rbody2d;

    private Rigidbody2D rbOther;

    private bool IsHid = false;
    private bool IsWalk = true;
    private bool IsHidAndCrazyMove = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        rbody2d = GetComponent<Rigidbody2D>();

        moveController = GetComponent<MobMove>();
    }

    private void Start()
    {
        oldSpeed = moveController.speed;
    }

    //private void Update()
    //{
    //    print(string.Format("IsHid = {0}\nIsWalk = {1}", IsHid, IsWalk));
    //    Debug.Log("nIsHidAndCrazyMove = " + IsHidAndCrazyMove);
    //    Debug.Log(moveController.MoveRight);
    //}

    public void OnKickUp(GameObject obj)
    {

        if (obj.CompareTag("Player"))
        {
            if (!rbOther) rbOther = obj.GetComponent<Rigidbody2D>();

            rbOther.AddForce(new Vector2(0, 15.5f), ForceMode2D.Impulse);

            if (!IsHid)
            {
                moveController.speed = 0;
                IsHid = true;
                IsWalk = false;
                _animator.SetBool("IsHid", IsHid);
                _animator.SetBool("IsWalk", IsWalk);
                StartCoroutine(UnHid());
                return;
            }

            if (IsHid && !IsHidAndCrazyMove)
            {
                StopCoroutine(UnHid());
                moveController.speed = oldSpeed * 10;
                IsHidAndCrazyMove = true;
                return;
            }

            if (IsHidAndCrazyMove && IsHid)
            {
                moveController.speed = 0;
                IsHidAndCrazyMove = false;
                StartCoroutine(UnHid());
                return;
            }

        }
    }

    private IEnumerator UnHid()
    {
        yield return new WaitForSeconds(3f);

        if (!IsHidAndCrazyMove)
        {
            IsHid = false;
            IsWalk = false;
            _animator.SetBool("IsHid", IsHid);
            _animator.SetBool("IsWalk", IsWalk);
        }
    }

    private void GoWalk()
    {
        IsWalk = true;
        _animator.SetBool("IsWalk", IsWalk);
        moveController.speed = oldSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            bool rigth = moveController.RigthCheck();
            bool left = moveController.LeftCheck();

            if (IsHid && !IsHidAndCrazyMove && (rigth || left))
            {
                StopCoroutine(UnHid());

                if (rigth) rbody2d.AddForce(new Vector2(-1 * moveController.speed *10, 0), ForceMode2D.Force); //moveController.HorizontalOnDefault = -1;
                if (left)  rbody2d.AddForce(new Vector2(1 * moveController.speed *10, 0), ForceMode2D.Force);

                moveController.speed = oldSpeed * 10;
                IsHidAndCrazyMove = true;
                
                return;
            }
        }
        if (other.gameObject.CompareTag(Tags.Enemy) && other.relativeVelocity.magnitude > 20)
        {
            if (!IsHid) StartCoroutine(Die());
        }

    }

   private IEnumerator Die()
    {
        Destroy(damagepoint);
        _animator.SetBool("IsHid", true);
        _animator.SetBool("IsWalk", false);

        yield return new WaitForSeconds(0.04f);

        GetComponent<CircleCollider2D>().enabled = false;
        _animator.enabled = false;
        rbody2d.constraints = RigidbodyConstraints2D.None;
        transform.rotation = Quaternion.Euler(0, 0, 180);
        rbody2d.AddForce(new Vector2(0, 1 * powerImpulse), ForceMode2D.Impulse);
        GameController.instance.PlaySound("smb_stomp");
        moveController.enabled = false;
        rbody2d.gravityScale *= 3;
        Destroy(gameObject, 4.5f);
    }

    public override void Kill()
    {
        StartCoroutine(Die());
    }

}
