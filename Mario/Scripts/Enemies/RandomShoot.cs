using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomShoot : MonoBehaviour {

    [SerializeField]private GameObject projectilePrefab;
    [SerializeField]private Transform shootPos;

    public void onShoot ()
    {
        var n = Random.Range(0, 50);

        if (n >= 34)
        {
            var clone = Instantiate (projectilePrefab, shootPos.position, Quaternion.identity);

            clone.transform.localScale = transform.localScale;
        }
    }
}
