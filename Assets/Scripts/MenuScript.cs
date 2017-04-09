using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour {

	void Start ()
    {
		
	}
	
	void Update ()
    {
        if (Input.GetKey(KeyCode.R))
            UnityEngine.SceneManagement.SceneManager.LoadScene("StageSelect");
	}
}
