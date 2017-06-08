using System.Collections;
using UnityEngine;

public class CameraMove2D : MonoBehaviour {

    public Transform startLevel;
    public Transform endtLevel;

  //  public Vector2 minPos = new Vector2(9.48f, 7.0f);
  //  public Vector2 maxPos = new Vector2(198.48f, 7.0f);

    public Transform target;

    private Camera cam;
    private Rect rect;

    private float width;

    private void Awake ()
    {
        cam = GetComponent<Camera> ();

        width = cam.orthographicSize * cam.aspect; // Половина ширины камеры
    }

    private void Start ()
    {
        //yield return new WaitForSeconds (0.5f);

        if (!target)
            target = GameObject.FindGameObjectWithTag ("Player").transform;

    }


    void LateUpdate ()
    {
        var x = target.position.x;
        x = Mathf.Clamp(x, startLevel.position.x + width, endtLevel.position.x - width);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
	}
}
