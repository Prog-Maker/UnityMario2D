using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveBlock : MonoBehaviour
{

    [SerializeField] private LayerMask bulletlayerMask;
    [SerializeField] private GameObject fungusPrefab;
    [SerializeField] private Transform fungusPrefabSpawnPoint;
    [SerializeField] private PlatformEffector2D effector;

    [SerializeField] GameObject sprite;

    private bool IsEmpty = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (1 << collision.gameObject.layer == bulletlayerMask.value) Destroy(collision.gameObject);

        if (!IsEmpty)
        {
            IsEmpty = true;

            sprite.SetActive(true);

            effector.surfaceArc = 360;

            Instantiate(fungusPrefab, fungusPrefabSpawnPoint.position, fungusPrefabSpawnPoint.rotation);
        }
    }
}
