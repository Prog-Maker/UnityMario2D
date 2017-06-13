using UnityEngine;

public class OnCouch : AbstractBehavior {

    private bool couch;
    private Animator animator;
	
	void Update ()
    {
        var canCouch = inputState.GetButtonValue(inputButtons[0]);
        CanCouch (canCouch);
	}

    protected override void Awake ()
    {
        base.Awake ();
        animator = GetComponent<Animator> ();
    }


    void CanCouch(bool value)
    {
        couch = value;
        animator.SetInteger ("CouchAnimState", couch ? 1 : 0);
    }
}
