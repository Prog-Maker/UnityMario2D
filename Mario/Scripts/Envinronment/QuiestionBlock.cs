using UnityEngine;

public class QuiestionBlock : BlockBase
{

    [Header("Fields")]
    [SerializeField] private Transform CoinOrFungusSpawnPoint;
    [SerializeField] private GameObject CoinOrFungusPrefab;
    [SerializeField] private Sprite EmptyBlock;

    private Animator _animator;
    private Rigidbody2D rbody;


    [Header("FOR TEST!! TRUE IS DEFAULT")]
    [SerializeField] bool EditroFieldIsEmpty = true;

    private bool IsEmpty = false;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsEmpty)
        {
            MoveBlock(rbody);

            IsEmpty = EditroFieldIsEmpty;

            _animator.SetBool("IsEmpty", IsEmpty);

            Instantiate(CoinOrFungusPrefab, CoinOrFungusSpawnPoint.position, CoinOrFungusSpawnPoint.rotation);
        }
    }

    
}
