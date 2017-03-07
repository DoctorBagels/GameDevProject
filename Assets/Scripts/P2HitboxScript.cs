using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2HitboxScript : MonoBehaviour
{

	void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "P1Headbox" && !P2Script.Ded && !P1Script.Ded && !P2Script.Charge)
        {
            P1Script.EnemyScore++;
            P1Script.Ded = true;
            P2Script.JVel = .5f;
            P2Script.Jumping = true;
        }
    } 
}
