using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRate : MonoBehaviour {
    public void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
