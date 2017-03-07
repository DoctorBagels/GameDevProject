using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Counter : MonoBehaviour
{
    TextMesh txt;
	
	void Start ()
    {
        txt = GetComponent<TextMesh>();
	}
	
	void Update ()
    {
        txt.text = "P1: " + P2Script.EnemyScore.ToString();
	}
}
