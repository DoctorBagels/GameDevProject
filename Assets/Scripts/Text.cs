using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text : MonoBehaviour {

	void Start ()
    {
		
	}
	
	void Update ()
    {
		if (Input.GetKeyDown(KeyCode.R) && CursorScript.Stage1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GAME");
            P1Script.Ded = true;
            P2Script.Ded = true;
        }

        if (Input.GetKeyDown(KeyCode.R) && CursorScript.Stage2)
        {
            P1Script.Ded = true;
            P2Script.Ded = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Stage 2");
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.B))
        {
            P1Script.Ded = true;
            P2Script.Ded = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Start Screen");
            CursorScript.Stage1 = true;
            CursorScript.Stage2 = false;
        }
	}
}
