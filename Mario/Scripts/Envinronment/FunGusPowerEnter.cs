using UnityEngine;

[RequireComponent(typeof(FunGus))]
public class FunGusPowerEnter : MonoBehaviour
{

    private FunGus fungus;

    private void Start()
    {
        fungus = GetComponent<FunGus>();
    }


    private void OnCollisionEnter2D(Collision2D _collision)
    {
        if (fungus.IsSpawn && _collision.gameObject.CompareTag(Tags.Player))
        {
            //CurrentCharacter.character.OnFungusPowerEnter();
            GameController.instance.Character.OnFungusPowerEnter();
        }
    }
}
