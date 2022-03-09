using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VirtualTrail : MonoBehaviour
{
    private Vector3 startpos;
    static VirtualTrail instance=null;
    // Start is called before the first frame update
    public static VirtualTrail Instance
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
    private void Awake() {
        instance=this;
    }
    public void InitInstance(Vector3 pos)
    {
        this.gameObject.SetActive(true);
        this.transform.GetChild(0).gameObject.SetActive(true);
        startpos=pos;
        this.transform.position=startpos;
    }
    public void UpdateInstance(Vector3 pos)
    {
        this.GetComponent<RectTransform>().rotation= Quaternion.Euler(0,0,Mathf.Atan2(pos.y-startpos.y,pos.x-startpos.x)*180/Mathf.PI-90);
    }
    public void UnableInstance()
    {
        this.gameObject.SetActive(false);
    }
}
