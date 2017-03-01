using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnBlock : BlockBase
{


    [SerializeField] private Transform CoinOrFungusSpawnPoint;
    [SerializeField] private GameObject CoinOrFungusPrefab;

    private Transform IsFull;
    private Transform IsEmpty;

    private Rigidbody2D rbody;

    private bool ISEmpty = false;

    private int coinsCount = 10;

    void Awake()
    {
        IsFull = transform.FindChild("IsFull");
        IsEmpty = transform.FindChild("IsEmpty");

        rbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!ISEmpty)
        {
            if (coinsCount > 0)
            {
                MoveBlock(rbody);

                Instantiate(CoinOrFungusPrefab, CoinOrFungusSpawnPoint.position, CoinOrFungusSpawnPoint.rotation);

                coinsCount--;
            }
            else
            {
                ISEmpty = true;
                IsEmpty.gameObject.SetActive(ISEmpty);
                IsFull.gameObject.SetActive(!ISEmpty);
            }
        }
    }



}
