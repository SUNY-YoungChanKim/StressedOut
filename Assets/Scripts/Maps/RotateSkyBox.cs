using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkyBox : MonoBehaviour
{
    float degree;

	void Start ()
    {
        Time.timeScale=1.0f;
        degree = 0;
    }
	
	void Update ()
    {
        degree += Time.deltaTime;
        if (degree >= 360)
            degree = 0;

        RenderSettings.skybox.SetFloat("_Rotation", degree);
    }
}
