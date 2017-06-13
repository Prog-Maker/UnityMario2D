using UnityEngine;

public class Standing : AbstractBehavior
{

    private Animator _animator;

    protected override void Awake ()
    {
        base.Awake ();
        _animator = GetComponent<Animator> ();
    }


    void Update ()
    {
        var stand = collisionState.standing;
        _animator.SetInteger ("StandAnimState", stand ? 1 : 0);
	}
}
