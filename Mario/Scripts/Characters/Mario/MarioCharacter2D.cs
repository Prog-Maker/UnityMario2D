using Assets.Mario.Scripts;
using System;
using System.Collections;
using UnityEngine;

namespace MarioWorldForAll
{

    public class MarioCharacter2D : CharacterBase
    {
        [SerializeField] private float runSpeed;
        [SerializeField] private float jumpPower;
        [SerializeField] private bool AirControl = false; // Можно ли двигаться в прыжке

        // [SerializeField] LayerMask layerMask;

        // [SerializeField] float rayCastHeigth;

        private Transform groundChecker;
        private Transform head;


        //[SerializeField] private float velocityToDown = 2.9f;

        private bool OnGround;
        private bool Jumped = false;
        private bool Moving = false;
        private bool Down = false;

        [SerializeField] public bool isSmall = true;


        [Header("Sounds")]
        [SerializeField] private AudioClip[] sounds;

        [Header("Sprites")]
        [SerializeField] private Sprite[] sprites;

        private Rigidbody2D rbody2d;
        private Animator animator;
        private SpriteRenderer srenderer;
        private CapsuleCollider2D _collider;
        [SerializeField] private AudioSource _audioPlayer;

        private float MultySpeed;

        bool isAnimatedToBig = false;
        bool isAnimatedToSmall = true;

        public static MarioCharacter2D instance;

        void Awake()
        {
           rbody2d = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            srenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<CapsuleCollider2D>();
            _audioPlayer = GetComponent<AudioSource>();

            head = transform.Find("Head");
            groundChecker = transform.Find("GroundChecker");

            kickPower = 0;

            if (instance == null) instance = this;
            else if (instance != this) Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            GroundCheck();
            Animate();

           /* print(transform.gameObject.activeInHierarchy);
            print("AudioSource - " + _audioPlayer.isActiveAndEnabled);
            print("Animator - " + animator.isActiveAndEnabled);*/
        }

        public void Move(float hDir, float vDir, bool jump)
        {
            Moving = (hDir != 0 ? true : false);

            Down = ((vDir < 0 && !isSmall) ? true : false);

            if ((OnGround || AirControl))
            {
                if (Moving) srenderer.flipX = Flip(hDir);

                //if (!Down)
                {
                    rbody2d.velocity = new Vector2(hDir * runSpeed, rbody2d.velocity.y);
                }

                //if (Down && rbody2d.velocity.y != 0)
                //{
                //    rbody2d.velocity = new Vector2(hDir * runSpeed, rbody2d.velocity.y);
                //}

                MultySpeed = hDir;
            }

            if (OnGround && jump)
            {
                if (isSmall) _audioPlayer.PlayOneShot(sounds[(int)Sounds.jumpSmall]);
                else _audioPlayer.PlayOneShot(sounds[(int)Sounds.jumpSuper]);

                // rbody2d.velocity = new Vector2(rbody2d.velocity.x / 2, rbody2d.velocity.y);
                rbody2d.AddForce(new Vector2(0f, jumpPower), ForceMode2D.Impulse);
            }

            //if (Down && rbody2d.velocity.x > 0 && rbody2d.velocity.y == 0)
            //{
            //    rbody2d.velocity = new Vector2(rbody2d.velocity.x - 0.3f, rbody2d.velocity.y);
            //}

            //if (Down && rbody2d.velocity.x < 0 && rbody2d.velocity.y == 0)
            //{
            //    rbody2d.velocity = new Vector2(rbody2d.velocity.x + 0.3f, rbody2d.velocity.y);
            //}


            // TestInformation.Info = rbody2d.velocity.x.ToString();
        }

        private void GroundCheck()
        {
            Collider2D[] _colliders = new Collider2D[5];
            var res = Physics2D.OverlapCircleNonAlloc(groundChecker.position, 0.2f, _colliders);

            if (res > 1)
            {
                foreach (var item in _colliders)
                {
                    if (item != null && item.gameObject.CompareTag(Tags.Ground))
                    {
                        OnGround = true;
                        Jumped = false;

                        if (rbody2d.gravityScale > 9)
                        {
                            rbody2d.gravityScale = 6;
                        }

                        return;
                    }
                }
            }
            OnGround = false;
            Jumped = true;
        }

        private void Animate()
        {
            animator.SetBool("isAnimatedToBig", isAnimatedToBig);
            animator.SetBool("isAnimatedToSmall", isAnimatedToSmall);
            animator.SetBool("OnGround", OnGround);
            animator.SetBool("Moving", Moving);
            animator.SetFloat("MultySpeed", MultySpeed);
            animator.SetBool("Jumped", Jumped);
            animator.SetBool("IsSmall", isSmall);
            animator.SetBool("Down", Down);

        }

        public override void OnFungusPowerEnter()
        {
            isSmall = false;
            if (!isSmall)
            {
                _audioPlayer.PlayOneShot(sounds[(int)Sounds.powerUp]);

                StartCoroutine(ToBig());

                _collider.offset = new Vector2(0f, 0.39f);
                _collider.size = new Vector2(0.77f, 1.81f);
                head.localPosition = new Vector2(0f, 1.49f);
                kickPower = 1;
            }
        }

        public override void OnGoombasEnter()
        {

            if (!isSmall)
            {
                isSmall = true;
                _audioPlayer.PlayOneShot(sounds[(int)Sounds.pipe]);

                StartCoroutine(ToSmall());

                _collider.offset = Vector2.zero;
                _collider.size = new Vector2(0.77f, 1.0f);
                head.localPosition = new Vector2(0f, 0.713f);
                kickPower = 0;
            }
            else if (isSmall)
            {
                GameController.instance.IsGameOver = true;

                StartCoroutine(marioGameOver());
            }

        }

        private IEnumerator marioGameOver()
        {
            //yield return new WaitForSeconds(0.5f);

            while (true)
            {
                _audioPlayer.PlayOneShot(sounds[(int)Sounds.marioDie]);
                

                rbody2d.Sleep();
                rbody2d.constraints = RigidbodyConstraints2D.FreezePositionX;

                srenderer.sprite = sprites[0];

                yield return new WaitForSeconds(1f);

                rbody2d.AddForce(Vector2.up * 2.5f, ForceMode2D.Impulse);
                GetComponent<CapsuleCollider2D>().enabled = false;

                break;
            }

            Destroy(gameObject, 1f);

            Restarter.instance.ReStart(0);
        }

        private IEnumerator ToSmall()
        {
            while (!isAnimatedToSmall)
            {

                rbody2d.Sleep();
                rbody2d.constraints = RigidbodyConstraints2D.FreezePosition;

                animator.SetTrigger("ToSmall");

                yield return new WaitForSeconds(1f);

                break;
            }

            isAnimatedToSmall = true;
            rbody2d.constraints =(RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation);
            isAnimatedToBig = false;
        }

        private IEnumerator ToBig()
        {

            while (!isAnimatedToBig)
            {
                rbody2d.Sleep();
                rbody2d.constraints = RigidbodyConstraints2D.FreezePosition;

                animator.SetTrigger("ToBig");

                yield return new WaitForSeconds(1f);

                break;
            }

            isAnimatedToBig = true;
            rbody2d.constraints = (RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation);
            isAnimatedToSmall = false;
        }

        bool Flip(float direction)
        {
            if (direction < 0) return true;
            return false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            rbody2d.gravityScale = 10;
        }

    }

    //Head :  small: Y = 0.489, big: Y = 1.49

    //Collider  small - offset Y = 0.0, y = 0.00
    //            big - offset Y = 0.39, y = 1.81

    enum Sounds
    {
        jumpSmall = 0,
        jumpSuper,
        stomp, // плавать (этот же звук убил моба)
        marioDie,
        fire,
        powerUp,
        pipe // стал малеьнкий (этот же звук залез в трубу)
    }

    //enum layerMask
    //{
    //    Enemny = 10
    //}

}