using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Script : MonoBehaviour
{
    Rigidbody2D RBP2;
    public static bool Jumping = false;
    public static float Vel;
    public float MaxVel;
    bool Right;
    bool Left;
    public static float JVel;
    int ChargeCounter;
    int Cooldown = 0;
    public static float EnemyScore;
    public static bool Charge;
    bool LCharge;
    bool RCharge;
    bool Hitstun;
    float SFrames;
    public static bool Ded = true;
    float DedTime = 0;

    void Start ()
    {
        RBP2 = GetComponent <Rigidbody2D>();
	}
	
	void Update ()
    {
        Movement();

        if (!Ded)
        {
            Jumps();
            Charges();
        }

        else
        {
            if (DedTime < 1)
            {
                transform.position = new Vector3(6, 3);
                Vel = 0;
                Jumping = false;
                JVel = 0;
                DedTime += Time.deltaTime;
            }

            if (DedTime >= 1)
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
            UnityEngine.SceneManagement.SceneManager.LoadScene("P1Win");
            EnemyScore = 0;
            P1Script.EnemyScore = 0;
        }

        if (!Jumping && !Hitstun)
        {
            RBP2.drag = 50;
        }

        else
        {
            RBP2.drag = 5;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Death Plane")
        {
            EnemyScore+= .5f;
            Ded = true;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Stage" && transform.position.y > -4)
        {
            Jumping = false;
            JVel = 0;
        }

        if (col.transform.name == "Player 1")
        {
            RBP2.AddForce(new Vector2(70 * P1Script.Vel, 0), ForceMode2D.Impulse);
            Debug.Log("P2Hit");
            Hitstun = true;
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
        if (Input.GetKey(KeyCode.RightArrow))
            Right = true;
        if (Input.GetKey(KeyCode.LeftArrow))
            Left = true;
        if (Input.GetKeyUp(KeyCode.RightArrow))
            Right = false;
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            Left = false;

        if (!Charge && !Ded)
        {
            if (Right && !Hitstun)
                Vel += .05f;
            if (Left && !Hitstun)
                Vel -= .05f;
            if (!Right && !Left && Vel >= 0 && !Hitstun)
            {
                Vel -= .025f;
                if (Vel < .025f)
                    Vel = 0;
            }
            if (!Right && !Left && Vel <= 0 && !Hitstun)
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

        if (Input.GetKeyDown(KeyCode.UpArrow) && !Jumping)
        {
            JVel = .5f;
            Jumping = true;
        }

        if (Jumping)
        {
            JVel -= .03f;
            transform.position += new Vector3(0, JVel, 0);
        }
    }

    void Charges()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Right && !Left && Cooldown == 0 && !RCharge)
        {
            RCharge = true;
            Charge = true;
            ChargeCounter = 6;
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

        if (Input.GetKeyDown(KeyCode.Space) && !Right && Left && Cooldown == 0 && !LCharge)
        {
            LCharge = true;
            Charge = true;
            ChargeCounter = 6;
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
}
