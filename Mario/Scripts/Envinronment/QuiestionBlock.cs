using UnityEngine;

public class QuiestionBlock : BlockBase
{

    [Header("Layer for Bullet")]
    [SerializeField]
    private LayerMask bulletlayerMask;

    [Header("Fields")]
    [SerializeField]
    private Transform CoinOrFungusSpawnPoint;
    [SerializeField] private GameObject CoinOrFungusPrefab;
    [SerializeField] private int CoinsCount;

    private Animator _animator;
    private Rigidbody2D rbody;


    [Header("FOR TEST!! TRUE IS DEFAULT")]
    [SerializeField]
    bool EditorFieldIsEmpty = true;

    private bool IsEmpty = false;

    void Awake ()
    {
        _animator = GetComponent<Animator> ();
        rbody = GetComponent<Rigidbody2D> ();
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (CoinsCount > 0)
        {
            CoinsCount--;

            if (CoinsCount <= 0) IsEmpty = true;

            if (1 << collision.gameObject.layer == bulletlayerMask.value) Destroy (collision.gameObject);

            if (!IsEmpty)
            {
                MoveBlock (rbody);

                Instantiate (CoinOrFungusPrefab, CoinOrFungusSpawnPoint.position, CoinOrFungusSpawnPoint.rotation);
            }

            _animator.SetBool ("IsEmpty", IsEmpty);
        }
    }


}
