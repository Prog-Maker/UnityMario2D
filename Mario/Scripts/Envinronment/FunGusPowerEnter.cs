using Assets.Mario.Scripts;
using UnityEngine;

//[RequireComponent(typeof(FunGus))]
public class FunGusPowerEnter : Collectable
{
    public int itemID = 1;
    //public GameObject projectilePrefab;

    override protected void OnCollect (GameObject target)
    {
        var changStateBevavior = target.GetComponent<ChangeState>();
        changStateBevavior.changeState (true);
        
        //var equipBehavior = target.GetComponent<Equip> ();
        //if (equipBehavior != null)
        //{
        //    equipBehavior.currentItem = itemID;
        //}

        //var shootBehavior = target.GetComponent<FireProjectile> ();
        //if (shootBehavior != null)
        //{
        //    shootBehavior.projectilePrefab = projectilePrefab;
        //}

    }


    //private FunGus fungus;

    //private void Start()
    //{
    //    fungus = GetComponent<FunGus>();
    //}


    //private void OnCollisionEnter2D(Collision2D _collision)
    //{
    //    if (fungus.IsSpawn && _collision.gameObject.CompareTag(Tags.Player))
    //    {
    //        //CurrentCharacter.character.OnFungusPowerEnter();
    //        GameController.instance.Character.OnFungusPowerEnter();
    //    }
    //}
}
