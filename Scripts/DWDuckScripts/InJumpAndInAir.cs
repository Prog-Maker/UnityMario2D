using UnityEngine;

public class InJumpAndInAir : AbstractBehavior {

    private Animator _animator;
    private float jumping;

    protected override void Awake ()
    {
        base.Awake ();
        _animator = GetComponent<Animator> ();
    }


    void Update ()
    {
        jumping = inputState.VelY;
        OnJump (jumping > 0.0f);
        OnAir (jumping < 0.0f);
	}


    void OnJump (bool value)
    {
        _animator.SetInteger ("JumpAnimState", value ? 1 : 0);
    }

    void OnAir(bool value)
    {
        _animator.SetInteger ("AirAnimState", value ? 1 : 0);
    }
}
