using UnityEngine;

public class RandomJump : MonoBehaviour {

    public float TimeMax;

    public float range1, range2;

    private Rigidbody2D rbody;

    private float currnetTime = 5f;

    private float time = 0.0f;


    private void Awake ()
    {
		rbody = GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate ()
    {
        time += Time.deltaTime;
        if (time >= currnetTime)
        {
            OnJump ();
        }
    }

    private void OnJump ()
    {
        rbody.velocity = new Vector2 (rbody.velocity.x, Random.Range (range1, range2));
        currnetTime = Random.Range (0.5f, TimeMax);
        time = 0.0f;
    }

}
