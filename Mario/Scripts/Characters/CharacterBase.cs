using UnityEngine;

namespace MarioWorldForAll
{

    public abstract class CharacterBase : MonoBehaviour
    {

        public virtual void OnFungusPowerEnter()
        {
            Debug.Log("I am FungusPowerEnter");
        }

        public virtual void OnFungusLiveEnter()
        {
            Debug.Log("I am FungusLiveEnter");
        }

        public virtual void OnGoombasEnter()
        {
            Debug.Log("I am GoomBasEnter");
        }

        public virtual void OnKoopasEnter()
        {
            Debug.Log("I am KoopasEnter");
        }

        public int kickPower { get; protected set; }
    }
}