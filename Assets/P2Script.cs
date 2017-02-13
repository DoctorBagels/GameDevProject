using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Script : MonoBehaviour
{
    Rigidbody2D RBP2;
    bool Jumping = false;
    public static float Vel;
    public float MaxVel;
    bool Right;
    bool Left;
    float JVel;
    int ChargeCounter;
    int Cooldown = 0;
    int EnemyScore;
    public static bool Charge;
    bool LCharge;
    bool RCharge;
    bool Hitstun;
    float SFrames;

    void Start ()
    {
        RBP2 = GetComponent <Rigidbody2D>();
	}
	
	void Update ()
    {
        Movement();
        Jumps();
        Charges();

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

        if (EnemyScore == 3)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("P1Win");
            EnemyScore = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Death Plane")
        {
            EnemyScore++;
            transform.position = new Vector3(6, 0);
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

        if (Hitstun)
            Vel = 0;

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
                transform.position += new Vector3(.5f, 0, 0);
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
                transform.position += new Vector3(-.5f, 0, 0);
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
