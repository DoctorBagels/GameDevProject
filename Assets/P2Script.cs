using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Script : MonoBehaviour
{
    public Vector3 Velocity = new Vector2(0.002f, 0);
    Rigidbody2D RBP2;
    bool Jumping = false;

	void Start ()
    {
        RBP2 = GetComponent <Rigidbody2D>();
	}
	
	void Update ()
    {
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 where = transform.position;
            where += Velocity;
            transform.position = where;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
             Vector3 where = transform.position;
             where -= Velocity;
             transform.position = where;
            
        }
        if (Input.GetKey(KeyCode.Space) && !Jumping)
        {
            RBP2.AddForce(new Vector2(0, 20), ForceMode2D.Impulse);
            Jumping = true;
        }

        
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "Death Plane")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("OVER");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.tag == "Stage")
        {
            Jumping = false;
        }
    }
}
