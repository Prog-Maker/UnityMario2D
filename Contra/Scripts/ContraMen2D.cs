using System.Collections;
using Assets.Mario.Scripts;
using MarioWorldForAll;
using UnityEngine;

public class ContraMen2D : CharacterBase
{

    [SerializeField] private float runSpeed = 2.5f;
    [SerializeField] private float jumpPower = 22.5f;

    [SerializeField] private float shootDelay = 1f;

    [SerializeField] Transform ShootPointsPool;
    [SerializeField] Transform[] ShootPoints;
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] Transform[] Fires;

    [SerializeField] private LayerMask wathIsGround;

    private bool AirControl = true;

    private Transform groundChecker;

    private Rigidbody2D rbody2d;
    private Animator[] animators;
    private SpriteRenderer[] srenderers;

    private bool OnGround;
    private bool Jumped;
    private bool Moving;
    private bool KeyDown;
    private bool Shooting;

    private float VSP;
    private int Direction;

    private float shootDelayCounter;

    public GameObject currentBullet;
    public Transform currentShootPoint;

    void Awake()
    {
        rbody2d = GetComponent<Rigidbody2D>();

        animators = GetComponentsInChildren<Animator>();

        srenderers = GetComponentsInChildren<SpriteRenderer>();

        groundChecker = transform.Find("GroundChecker");

        currentBullet = BulletPrefab;

        currentShootPoint = ShootPoints[2];

        for (int i = 0; i < Fires.Length; i++)
        {
            Fires[i] = Instantiate(Fires[i]);
            Fires[i].gameObject.SetActive(false);
        }
    }


    void Update()
    {
        GroundCheck();
        VSP = rbody2d.velocity.y;
        Animate();
    }

    public void Move(float hDir, float vDir, bool jump)
    {
        Moving = (hDir != 0 ? true : false);

        KeyDown = ((vDir < 0) ? true : false);


        if (hDir != 0 && vDir > 0)
        {
            Direction = 7;

            if (rbody2d.velocity.x > 0) ShootPoints[1].localRotation = Quaternion.Euler(0f, 0f, 25f);
            else ShootPoints[1].localRotation = Quaternion.Euler(0f, 0f, 40f);

            currentShootPoint = ShootPoints[1]; // Под углом вверх
            //Fire.transform.position = currentShootPoint.position;
           // Fire.transform.localRotation = currentShootPoint.localRotation;
        }

        if (hDir != 0 && vDir < 0)
        {
            Direction = 1;


            if (rbody2d.velocity.x > 0) ShootPoints[3].localRotation = Quaternion.Euler(0f, 0f, -25f);
            else ShootPoints[1].localRotation = Quaternion.Euler(0f, 0f, -40f);

            currentShootPoint = ShootPoints[3];  // Под углом вниз
            //Fire.transform.position = currentShootPoint.position;
        }

        if (hDir != 0 && vDir == 0)
        {
            Direction = 0;
            currentShootPoint = ShootPoints[2];  // Стреляем прямо
           // Fire.transform.position = currentShootPoint.position;
        }

        if (hDir == 0 && vDir > 0)
        {
            Direction = 8;
            currentShootPoint = ShootPoints[0]; // Стреляем вверх
        }

        if (hDir == 0 && vDir < 0)
        {
            Direction = 0;
            currentShootPoint = ShootPoints[4]; // Стреляем лежа
        }

        if (hDir == 0 && vDir == 0)
        {
            Direction = 0;
            currentShootPoint = ShootPoints[2];  // Стреляем прямо
        }


        if ((OnGround || AirControl))
        {
            if (Moving)
            {
                float euler = (hDir < 0 ? 180f : 0);
                ShootPointsPool.rotation = Quaternion.Euler(0f, euler, 0f);
                srenderers[0].flipX = Flip(hDir);
                srenderers[1].flipX = Flip(hDir);
            }

            rbody2d.velocity = new Vector2(hDir * runSpeed, rbody2d.velocity.y);

            // MultySpeed = hDir;
        }

        if (OnGround && jump)
        {
            rbody2d.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
        }

    }


    public void Shoot(bool shoot)
    {
        Shooting = shoot;
        if (shoot && shootDelayCounter <= 0)
        {
            StartCoroutine(viewFire());
            Instantiate(currentBullet, currentShootPoint.position, currentShootPoint.rotation);
            shootDelayCounter = shootDelay;
        }
        shootDelayCounter -= Time.deltaTime;
        
    }

    private IEnumerator viewFire()
    {
        //GameObject gO = Fires[Random.Range(0, Fires.Length)].gameObject;

        //gO.transform.position = currentShootPoint.position;

        //gO.transform.rotation = currentShootPoint.rotation;

        //gO.SetActive(true);
        Fires[2].transform.position = currentShootPoint.position;

        Fires[2].rotation = currentShootPoint.rotation;

        Fires[2].gameObject.SetActive(true);

        yield return new WaitForSeconds(0.05f);

        Fires[2].gameObject.SetActive(false);
        
    }

    private void GroundCheck()
    {
        Collider2D[] _colliders = new Collider2D[3];
        var res = Physics2D.OverlapCircleNonAlloc(groundChecker.position, 0.2f, _colliders, wathIsGround);

        if (res > 0)
        {
            foreach (var item in _colliders)
            {
                if (item != null && item.gameObject.CompareTag(Tags.Ground))
                {
                    OnGround = true;
                    Jumped = false;

                    return;
                }
            }
        }
        OnGround = false;
        Jumped = true;
    }

    private bool Flip(float direction)
    {
        if (direction < 0) return true;
        return false;
    }

    private void Animate()
    {
        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].SetBool("OnGround", OnGround);
            animators[i].SetBool("Jumped", Jumped);
            animators[i].SetBool("Moving", Moving);
            animators[i].SetBool("Shooting", Shooting);
            animators[i].SetBool("KeyDown", KeyDown);
            animators[i].SetFloat("VSP", VSP);
            animators[i].SetInteger("Direction", Direction);
        }
    }
}
