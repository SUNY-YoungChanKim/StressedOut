using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]private float Magnitude;
    private void FixedUpdate() 
    {
        this.transform.Rotate(new Vector3(0,Magnitude,0));    
    }
}
