using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2HitboxScript : MonoBehaviour
{
    public ParticleSystem Prefab;
    public ParticleSystem PrefabAlt;

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
            Instantiate(PrefabAlt, new Vector3(P1Script.XPos, P1Script.YPos), Quaternion.identity);
            Instantiate(Prefab, new Vector3(transform.position.x, transform.position.y-.05f), Quaternion.identity);
            P1Script.EnemyScore++;
            P1Script.Ded = true;
            P2Script.JVel = .5f;
            P2Script.Jumping = true;
        }
    } 
}
