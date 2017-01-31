using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text : MonoBehaviour {

	void Start ()
    {
		
	}
	
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GAME");
        }
	}
}
