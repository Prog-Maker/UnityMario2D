using UnityEngine;
using MarioWorldForAll;

public class LowKickBreakBlock : BlockBase
{

    [SerializeField] private GameObject BreakEffectPrefab;

    [SerializeField] private LayerMask bulletlayerMask;

    private Rigidbody2D rbody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    public void OnKick(Collider2D other)
    {

        if (other.isTrigger)
        {
            //Debug.Log(other.gameObject.name);

            try
            {
                CharacterBase ch = other.transform.parent.gameObject.GetComponent<CharacterBase>();

                if (ch)
                {
                    if (ch.kickPower > 0)
                    {
                        gameObject.SetActive(false);
                        GameController.instance.PlaySound("smb_breakblock");
                        Instantiate(BreakEffectPrefab, transform.position, transform.rotation);
                        Destroy(gameObject);
                    }
                    else
                    {
                        GameController.instance.PlaySound("smb_bump");
                        MoveBlock(rbody); // движение блока вверх вниз пока марио маленький
                    }
                }
            }
            catch
            {
                return;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (1 << other.gameObject.layer == bulletlayerMask.value)
        {
            gameObject.SetActive(false);
            GameController.instance.PlaySound("smb_breakblock");
            Instantiate(BreakEffectPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }



    //private IEnumerator Move()
    //{
    //    bool up = true;

    //    yield return new WaitForSeconds(0.05f);

    //    while (true)
    //    {

    //        if (up)
    //        {
    //            transform.position = new Vector3(transform.position.x, transform.position.y + verticalDistanceMove, transform.position.z);

    //            yield return new WaitForSeconds(0.05f);
    //            up = !up;
    //        }

    //        if (!up)
    //        {
    //            transform.position = new Vector3(transform.position.x, transform.position.y - verticalDistanceMove, transform.position.z);

    //            yield return new WaitForSeconds(0.05f);

    //            break;
    //        }

    //    }
    //}

}
