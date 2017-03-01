using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FunGus : MonoBehaviour
{
    private Rigidbody2D rbody2d;

    public bool IsSpawn = false;
    private float speedUp = 1.8f;

   // [SerializeField] float jumppower;

    void Awake()
    {
        rbody2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        IsSpawn = false;
        GameController.instance.PlaySound("smb_vine");
        StartCoroutine(SpawnStart());
    }

    IEnumerator SpawnStart()
    {
        //yield return new WaitForSeconds(0.5f);

        while (!IsSpawn)
        {
            rbody2d.velocity = new Vector2(0, speedUp);

            yield return new WaitForSeconds(0.3f);

            rbody2d.isKinematic = false;

            //rbody2d.AddForce(Vector2.up * jumppower, ForceMode2D.Impulse);
            IsSpawn = true;

            break;
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log(other.gameObject.name);

        if (other.gameObject.CompareTag(Tags.Player) && IsSpawn)
        {
            //Debug.Log("Destroy");
            Destroy(gameObject);
        }
    }


}
