using UnityEngine;
using System.Collections;

public class Equip : AbstractBehavior
{

    public string animatorState = "";
    private int _currentItem = 0;
	private Animator animator;

    private ChangeState changeBehavior;

	public int currentItem
    {
		get{ return _currentItem;}
		set
        {
			_currentItem = value;
			animator.SetInteger(animatorState, _currentItem);
		}

	}

	override protected void Awake()
    {
		base.Awake ();
		animator = GetComponent<Animator> ();
        changeBehavior = GetComponent<ChangeState> ();
	}


    private void OnChangeState (int state)
    {
        currentItem = state;
        changeBehavior.changeState (false);
    }

}
