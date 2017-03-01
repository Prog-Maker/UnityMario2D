using MarioWorldForAll;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Koopa : Enemy {

    [SerializeField] private LayerMask damagelayer;

    private MobMove moveController;

    private float oldSpeed;

    private Animator _animator;
    private Rigidbody2D rbody2d;

    private bool IsHid = false;
    private bool IsWalk = true;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        rbody2d = GetComponent<Rigidbody2D>();
        moveController = GetComponent<MobMove>();
    }

    public void OnKickUp(GameObject obj)
    {
        if (obj.CompareTag("Player") && !IsHid)
        {
            oldSpeed = moveController.speed;
            moveController.speed = 0;
            IsHid = true;
            IsWalk = false;
            _animator.SetBool("IsHid", IsHid);
            _animator.SetBool("IsWalk", IsWalk);
            StartCoroutine(UnHid());
        }
    }

    private IEnumerator UnHid()
    {
        yield return new WaitForSeconds(3f);

        IsHid = false;
        IsWalk = false;
        _animator.SetBool("IsHid", IsHid);
        _animator.SetBool("IsWalk", IsWalk);
        
    }

    private void GoWalk()
    {
        IsWalk = true;
        _animator.SetBool("IsWalk", IsWalk);
        moveController.speed = oldSpeed;
    }

}
