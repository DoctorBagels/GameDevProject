using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshake : MonoBehaviour {

	void Start ()
    {
		
	}
	

	void Update ()
    {
		
	}

    public void Shake()
    {
        StartCoroutine("Screenshaker");
    }

    public IEnumerator Screenshaker()
    {
        float time = 0.15f;
        while (time > 0.0f)
        {
            Debug.Log(time);
            Camera.main.transform.position = (Vector3)Random.insideUnitCircle + Vector3.back * 10.0f;
            time -= Time.deltaTime;
            yield return 0;
        }
        Camera.main.transform.position = new Vector3(0.0f, 0.0f, -10.0f);

    }

}
