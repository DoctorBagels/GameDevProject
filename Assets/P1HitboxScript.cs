using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1HitboxScript : MonoBehaviour
{

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "P2Headbox" && !P1Script.Ded && !P2Script.Ded)
        {
            P2Script.EnemyScore++;
            P2Script.Ded = true;
            P1Script.JVel = .5f;
            P1Script.Jumping = true;
        }                             
    }
}
