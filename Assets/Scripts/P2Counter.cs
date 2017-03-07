using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2Counter : MonoBehaviour
{

    TextMesh txt;

	void Start ()
    {
        txt = GetComponent<TextMesh>();
	}
	
	void Update ()
    {
        txt.text = "P2: " + P1Script.EnemyScore.ToString();
	}
}
