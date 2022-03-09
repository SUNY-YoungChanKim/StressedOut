using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniping : MonoBehaviour
{
    [SerializeField]private float FireInterval;
    [SerializeField]private float AimInterval;
     [SerializeField]private float ShootTimeInterval;

    [SerializeField]private float AimSpeed;
    [SerializeField]private GameObject Bullet;

    private Vector3 FollowPos,OriginPos;
    LineRenderer LineRender;
    private bool isAming=false;
    // Start is called before the first frame update

    private void Start()
    {
        LineRender=this.GetComponent<LineRenderer>();
        LineRender.SetPosition(0,this.transform.position);
    }
    private void OnEnable() 
    {
        Invoke("Aim",1);
    }
    private void OnDisable() 
    {
                CancelInvoke();
    }
    // Update is called once per frame
     private void FixedUpdate()
    {
        OriginPos=new Vector3(this.transform.position.x,CharacterInfo.Instance.GetPos().y,this.transform.position.z);
        
        if(LineRender.enabled)
            LineRender.SetPosition(0,OriginPos);
        if(isAming==false)
        {
            FollowPos=Vector3.MoveTowards(FollowPos,CharacterInfo.Instance.GetPos(),AimSpeed);
            LineRender.SetPosition(1,(FollowPos-OriginPos)*1000);
        }
    
    }

    private void Aim()
    {
        LineRender.enabled=true;
        isAming=false;
        Invoke("FireReady",AimInterval);
    }
    private void FireReady()
    {
        isAming=true;
        Invoke("Fire",ShootTimeInterval);
    }
    private void Fire()
    {
        GameObject t = Instantiate(Bullet,OriginPos,this.transform.rotation);
        t.GetComponent<EnemyBullet>().Set((FollowPos-OriginPos)*1000,transform.parent.GetComponent<MonsterInfo>().GetDamage());
        LineRender.enabled=false;
        Invoke("Aim",FireInterval);
    }
}
