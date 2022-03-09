using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualTrailImage : MonoBehaviour
{
    [SerializeField]private Color First,Second,Third;
    private static VirtualTrailImage instance;
    private RawImage RawI;
    private Vector3 OriginSize;
    // Start is called before the first frame update
    public static VirtualTrailImage Instance
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
    private void Awake() 
    {
        instance=this;
        RawI=this.GetComponent<RawImage>();
        OriginSize=this.transform.localScale;
    }
    public void ChangeColor(float Val)
    {
        if(Val<=1)
        {
            RawI.color=First;
        }
        else if(Val<=2)
        {
            RawI.color=Second;
        }
        else
        {
            RawI.color=Third;
        }
    }
    public void ChangeSize(float Val)
    {
        if(Val<=1)
        {

            this.transform.localScale=OriginSize;
        }
        else if(Val<=2)
        {

            this.transform.localScale=OriginSize*1.5f;
        }
        else
        {
            this.transform.localScale=OriginSize*2.0f;
        }
    }
}
