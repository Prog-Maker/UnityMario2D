
using UnityEngine;

public class OnDestroyGameObject : MonoBehaviour {

    public  void OnDestroy ()
    {
        Destroy (gameObject);
    }
}
