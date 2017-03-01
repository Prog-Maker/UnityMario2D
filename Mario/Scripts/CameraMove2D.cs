using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove2D : MonoBehaviour {


    public Vector2 minPos = new Vector2(9.48f, 7.0f);
    public Vector2 maxPos = new Vector2(198.48f, 7.0f);

    public Transform target;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);

        target = GameController.instance.Character.transform;
    }


    void Update()
    {
        var x = target.position.x;
        x = Mathf.Clamp(x, minPos.x, maxPos.x);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
	}
}
