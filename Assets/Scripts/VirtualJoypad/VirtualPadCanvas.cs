using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualPadCanvas : MonoBehaviour
{
    private static VirtualPadCanvas instance;

    
    public static VirtualPadCanvas Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        instance=this;   
    }

    // Update is called once per frame
    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
    public void Activate()
    {
        this.gameObject.SetActive(true);
    }
}
