using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTargeting : MonoBehaviour
{
    [SerializeField]private float DetectInterval=5.0f;

    private Vector3 Direction;
    private GameObject FollowMonster=null;
    // Start is called before the first frame update

    private void OnEnable() {
        Detect();
    }

    // Update is called once per frame
    void Update()
    {
        ValidCheck();
    }
    private void Detect()
    {
        float OriginAngle=this.transform.rotation.eulerAngles.y-90;
        float angle;
        RaycastHit hit;
        float Distance=float.MaxValue;

        FollowMonster=null;
        for(float i=0;i<360;i+=DetectInterval)
        {   
            angle=OriginAngle+i;
            Direction=new Vector3(Mathf.Sin(Mathf.Deg2Rad * angle),0,Mathf.Cos(Mathf.Deg2Rad * angle));
            Physics.Raycast(this.transform.position,Direction,out hit);

            if(hit.transform.CompareTag("Monster"))
            {
                if(Vector3.Distance(this.transform.position,hit.transform.position)<Distance)
                    FollowMonster=hit.transform.gameObject;
            }
        }

        if(FollowMonster==null)
            this.gameObject.SetActive(false);
    }
    private void ValidCheck()
    {
        if(FollowMonster.activeSelf==false||this.transform.position==FollowMonster.transform.position)Detect();
        else this.gameObject.GetComponent<Bullet>().SetTargetPos(FollowMonster.transform.position);
    }
}
