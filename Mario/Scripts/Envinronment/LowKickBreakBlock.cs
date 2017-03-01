using UnityEngine;
using MarioWorldForAll;

public class LowKickBreakBlock : BlockBase
{

    [SerializeField] private GameObject BreakEffectPrefab;

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

            var ch = other.transform.parent.gameObject.GetComponent<CharacterBase>();

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
