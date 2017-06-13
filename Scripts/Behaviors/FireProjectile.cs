using UnityEngine;

public class FireProjectile : AbstractBehavior {

	public float shootDelay = .5f;
	public GameObject projectilePrefab;
    public Transform shootPositionNormal;
    public Transform shootPositionDuck;

    [Tooltip("Нужно ли получать компонет Duck, для того чтобы изменять ShootPoint")]
    public bool needDuckBehavior;

    [HideInInspector]
    public bool shooting = false;

	private float timeElapsed = 0f;

    private Duck duckBehavoir;


    protected override void Awake ()
    {
        base.Awake ();
        if (needDuckBehavior)
        {
            duckBehavoir = GetComponent<Duck> ();
        }
    }

    void Update ()
    {
	
		if (projectilePrefab != null)
        {

			var canFire = inputState.GetButtonValue(inputButtons[0]);

			if(canFire && timeElapsed > shootDelay)
            {
                Vector2 pos = new Vector2();

                if (needDuckBehavior && duckBehavoir.ducking)
                {
                    if (shootPositionDuck)
                    pos = shootPositionDuck.position;
                }
                else
                {
                    pos = shootPositionNormal.position;
                }

                shooting = true;
                CreateProjectile (pos);
				timeElapsed = 0;
			}

			timeElapsed += Time.deltaTime;
		}

	}

	public void CreateProjectile(Vector2 pos)
    {
		var clone = Instantiate (projectilePrefab, pos, Quaternion.identity) as GameObject;
		clone.transform.localScale = transform.localScale;
	}

    public void CanselShoot ()
    {
        shooting = false;
    }
}
