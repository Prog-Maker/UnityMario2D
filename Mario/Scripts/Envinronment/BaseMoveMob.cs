using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMoveMob : MonoBehaviour
{

	public virtual void OnRayCastEnter()
    {
        Debug.Log(this.transform.parent.name);
    }
}
