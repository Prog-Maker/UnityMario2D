using MarioWorldForAll;
using UnityEngine;

public class Mario : CharacterBase
{

    private CircleCollider2D circ;
    private CapsuleCollider2D caps;

    public bool IsSuper;

	private void Awake ()
    {
        circ = GetComponent<CircleCollider2D> ();
        caps = GetComponent<CapsuleCollider2D> ();
	}


    public override void OnFungusPowerEnter ()
    {
        circ.enabled = false;
        caps.enabled = !circ.enabled;
        IsSuper = caps.enabled;
        GetComponent<FireProjectile> ().enabled = true;
    }

    //public void MarioSuper (bool value)
    //   {
    //       circ.enabled = value;
    //       caps.enabled = !value;
    //}
}
