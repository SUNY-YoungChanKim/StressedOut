using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraHandler : MonoBehaviour
{
    private static MainCameraHandler instance;

    public static MainCameraHandler Instance
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
    private void Awake() {
        instance=this;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void CallMoveEnvet(Vector3 Target,float Speed,float Wait)
    {
        StartCoroutine(Moveto(Target,Speed,Wait));
    }
    IEnumerator Moveto(Vector3 Target,float Speed,float Wait)
    {
        Time.timeScale=0;
        Vector3 point=new Vector3(Target.x,this.transform.position.y,Target.z)-new Vector3(0,0,25);

        while(true)
        {
            this.transform.position=Vector3.MoveTowards(this.transform.position,point,Speed);
            if(this.transform.position==point)
            {
                for(int i=0;i<Wait;i++)yield return new WaitForSecondsRealtime(1.0f);
                Time.timeScale=1;
                break;
            }
            else yield return null;

        }
    }
}
