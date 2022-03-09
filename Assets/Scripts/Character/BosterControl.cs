using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BosterControl : MonoBehaviour
{
    private ParticleSystem ParSys;

    private void Awake() 
    {
        ParSys=this.GetComponent<ParticleSystem>();    
    }
    // Update is called once per frame
    void Update()
    {
        if(VirtualJoypad.Instance.getMoveStatus())
        {
            ParSys.Play();
        }
        else
        {
            ParSys.Stop();
        }
        
    }
}
