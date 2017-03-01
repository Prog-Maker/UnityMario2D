using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickPointOnBlock : MonoBehaviour
{
    private LowKickBreakBlock script;

    void Awake ()
    {
        script = GetComponentInParent<LowKickBreakBlock>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger) return;

        script.OnKick(other);
    }
}
