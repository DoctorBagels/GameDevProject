using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    public static bool Stage1 = true;
    public static bool Stage2 = false;
    public static bool Back = false;
    float counter = .5f;
    int ypos = 2;
    int xpos = -3;
    float cooldown;

	void Start ()
    {
		
	}


    void Update()
    {
        transform.position = new Vector3(xpos, ypos);

        counter -= Time.deltaTime;
        if (counter <= 0 && xpos == -3)
        {
            xpos = -2;
            counter = .5f;
        }
        if (counter <= 0 && xpos == -2)
        {
            xpos = -3;
            counter = .5f;
        }

        if (cooldown > 0)
            cooldown--;

        if (Input.GetKeyDown(KeyCode.DownArrow) && Stage1 && cooldown == 0)
        {
            Stage2 = true;
            Stage1 = false;
            Back = false;
            cooldown = 5;
            xpos = -3;
            counter = .5f;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && Stage2 && cooldown == 0)
        {
            Back = true;
            Stage2 = false;
            Stage1 = false;
            cooldown = 5;
            xpos = -3;
            counter = .5f;
        }

        if (Input.GetKeyDown(KeyCode.C) && Back && cooldown == 0)
        {
            Stage2 = true;
            Back = false;
            Stage1 = false;
            cooldown = 5;
            counter = .5f;
            xpos = -3;
        }

        if (Input.GetKeyDown(KeyCode.C) && Stage2 && cooldown == 0)
        {
            Stage1 = true;
            Stage2 = false;
            Back = false;
            cooldown = 5;
            xpos = -3;
            counter = .5f;
        }

        if (Stage1)
            ypos = 2;
        if (Stage2)
            ypos = 0;
        if (Back)
            ypos = -2;


        if (Input.GetKeyDown(KeyCode.R) && Stage1)
            UnityEngine.SceneManagement.SceneManager.LoadScene("GAME");
        if (Input.GetKeyDown(KeyCode.R) && Stage2)
            UnityEngine.SceneManagement.SceneManager.LoadScene("Stage 2");
        if (Input.GetKeyDown(KeyCode.R) && Back)
            UnityEngine.SceneManagement.SceneManager.LoadScene("Start Screen");

    }
}
