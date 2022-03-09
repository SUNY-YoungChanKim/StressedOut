using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VirtualPadManage : MonoBehaviour
{
    private Animator Ani;
    private static VirtualPadManage instance=null;
    // Start is called before the first frame update
    private Vector3 originSize;

    public static VirtualPadManage Instance
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
        Ani=this.GetComponent<Animator>();  
        originSize=this.transform.localScale;
    }
    public void Popup(Vector3 pos)
    {
        this.transform.position=pos;
        Ani.SetInteger("PopState",1);
    }
    public void PopDown()
    {
        Ani.SetInteger("PopState",-1);
    }
    public void PopZero()
    {
        Ani.SetInteger("PopState",0);
    }
    public void ChangeSize(float Val)
    {
        if(Val<=1)
        {

            this.transform.localScale=originSize;
        }
        else if(Val<=2)
        {

            this.transform.localScale=originSize*1.5f;
        }
        else
        {

            this.transform.localScale=originSize*2.0f;
        }
    }
}
