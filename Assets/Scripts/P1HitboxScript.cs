using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1HitboxScript : MonoBehaviour
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
        if (col.name == "P2Headbox" && !P1Script.Ded && !P2Script.Ded && !P1Script.Charge)
        {
            Instantiate(PrefabAlt, new Vector3(P2Script.XPos, P2Script.YPos), Quaternion.identity);
            Instantiate(Prefab, new Vector3(transform.position.x, transform.position.y - .05f), Quaternion.identity);
            P2Script.EnemyScore++;
            P2Script.Ded = true;
            P1Script.JVel = .5f;
            P1Script.Jumping = true;
        }                             
    }
}
