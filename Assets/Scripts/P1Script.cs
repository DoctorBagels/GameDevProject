using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Script : MonoBehaviour
{

    Rigidbody2D RBP1;
    public static bool Jumping = false;
    public static float Vel;
    public float MaxVel;
    public static float JVel;
    int ChargeCounter;
    int Cooldown = 0;
    public static float EnemyScore;
    bool Right;
    bool Left;
    public static bool Charge;
    bool LCharge;
    bool RCharge;
    bool Hitstun;
    float SFrames;
    public static bool Ded = true;
    float DedTime = 1;
    public Sprite[] LSprites;
    public Sprite[] RSprites;
    public Sprite[] Sprites;
    public SpriteRenderer SR;
    int STimer = 0;
    bool Crouch;
    int Current = 0;
    bool Preded;
    public ParticleSystem Prefab;
    public ParticleSystem PrefabAlt;
    public static float XPos;
    public static float YPos;
    public AudioSource Jumpsound;
    public AudioSource Clink;
    public AudioSource Death;
    public AudioSource Ground;

    void Start()
    {
        RBP1 = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();

        Sprites = RSprites;
    }

    void Update()
    {
        Movement();
        Animation();

        if (!Ded)
        {
            Jumps();
            Charges();
        }

        else
        {
            if (DedTime < 1)
            {
                transform.position = new Vector3(20, 20);
                Vel = 0;
                Jumping = false;
                JVel = 0;
                DedTime += Time.deltaTime;
            }
            if (DedTime < 2 && DedTime >= 1)
            {
                transform.position = new Vector3(-6, 3);
                Vel = 0;
                Jumping = false;
                JVel = 0;
                DedTime += Time.deltaTime;
            }

            if (DedTime >= 2)
            {
                Ded = false;
                DedTime = 0;
                Jumping = true;
            }
        }

        if (Cooldown > 0)
            Cooldown--;
        if (ChargeCounter < 0)
            ChargeCounter = 0;

        if (Hitstun)
        {
            SFrames += Time.deltaTime;
            if (SFrames > .2)
            {
                Hitstun = false;
                SFrames = 0;
            }
        }

        if (EnemyScore >= 3)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("P2Win");
            EnemyScore = 0;
            P2Script.EnemyScore = 0;
        }

        if (!Jumping && !Hitstun)
        {
            RBP1.drag = 50;
        }

        else
        {
            RBP1.drag = 5;
        }

        XPos = transform.position.x;
        YPos = transform.position.y;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Death Plane")
        {
            Camera.main.GetComponent<Screenshake>().Shake();
            Instantiate(PrefabAlt, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            Instantiate(Prefab, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
            EnemyScore+= .5f;
            Ded = true;
            Death.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Stage" && transform.position.y > -4)
        {
            Ground.Play();
            Jumping = false;
            JVel = 0;
        }

        if (col.transform.name == "Player 2")
        {
            RBP1.AddForce(new Vector2(70 * P2Script.Vel, 0), ForceMode2D.Impulse);
            Debug.Log("HitP1");
            Hitstun = true;
            Clink.Play();
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.tag == "Stage")
        {
            Jumping = true;
        }
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.D))
            Right = true;
        if (Input.GetKey(KeyCode.A))
            Left = true;
        if (Input.GetKeyUp(KeyCode.D))
            Right = false;
        if (Input.GetKeyUp(KeyCode.A))
            Left = false;
        if (Input.GetKey(KeyCode.S) && !Ded && !Jumping)
            Crouch = true;
        if (Input.GetKeyUp(KeyCode.S))
            Crouch = false;

        if (!Charge && !Ded)
        {
            if (Right && !Hitstun && !Crouch)
                Vel += .05f;
            if (Left && !Hitstun && !Crouch)
                Vel -= .05f;
            if (!Right && !Left && Vel > 0 && !Hitstun || Crouch && Vel > 0)
            {
                Vel -= .025f;
                if (Vel < .025f)
                    Vel = 0;
            }
            if (!Right && !Left && Vel < 0 && !Hitstun || Crouch && Vel < 0)
            {
                Vel += .025f;
                if (Vel > -.025f)
                    Vel = 0;
            }

            if (Vel >= MaxVel)
                Vel = MaxVel;
            if (Vel <= -MaxVel)
                Vel = -MaxVel;
        }

        if (Hitstun)
            Vel = 0;

        transform.position += new Vector3(Vel, 0, 0);
    }

    void Jumps()
    {

        if (JVel <= -.4f)
            JVel = -.4f;

        if (Input.GetKeyDown(KeyCode.W) && !Jumping)
        {
            JVel = .5f;
            Jumping = true;
            Jumpsound.Play();
        }

        if (Jumping)
        {
            JVel -= .03f;
            transform.position += new Vector3(0, JVel, 0);
        }
    }

    void Charges()
    {
        if (Input.GetKeyDown(KeyCode.B) && Right && !Left && Cooldown == 0 && !RCharge)
        {
            RCharge = true;
            Charge = true;
            ChargeCounter = 8;
        }

        if (RCharge)
        {
            JVel = 0;

            if (ChargeCounter > 0)
            {
                Vel = .5f;
                ChargeCounter--;
            }

            if (ChargeCounter <= 0)
            {
                RCharge = false;
                Charge = false;
                Cooldown = 60;
            }
        }

        if (Input.GetKeyDown(KeyCode.B) && !Right && Left && Cooldown == 0 && !LCharge)
        {
            LCharge = true;
            Charge = true;
            ChargeCounter = 8;
        }

        if (LCharge)
        {
            JVel = 0;

            if (ChargeCounter > 0)
            {
                Vel = -.5f;
                ChargeCounter--;
            }

            if (ChargeCounter <= 0)
            {
                LCharge = false;
                Charge = false;
                Cooldown = 60;
            }
        }
    }

    void Animation()
    {
        if (Vel > 0)
            Sprites = RSprites;
        if (Vel < 0)
            Sprites = LSprites;

        if (Right && Vel > 0 || Left && Vel < 0)
        {
            STimer++;
            if (STimer > 6)
            {
                Current++;
                if (Current > 3)
                    Current = 0;
                STimer = 0;
            }
        }

        if (Crouch)
            Current = 4;
        else if (!Right && !Left)
            Current = 0;

        if (Jumping && !LCharge && !RCharge)
            Current = 5;

        if (LCharge)
        {
            Sprites = LSprites;
            Current = 7;
        }

        if (RCharge)
        {
            Sprites = RSprites;
            Current = 7;
        }

        //if (Right && !Left && Vel < 0 || Left && !Right && Vel > 0)
        //Current = 6;

        SR.sprite = Sprites[Current];
    }
}