using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   private int HitNum;
    Queue<GameObject> EffectQueueRef,BulletQueueRef;
    private float Damage,Speed,EffectDuration;
    private Vector3 TargetPos;
    private GameObject HitEffect;
    // Start is called before the first frame update
    public Bullet()
    {
  
    }
    public float GetDamage()
    {
        return Damage;
    }
    // Update is called once per frame
    private void FixedUpdate() 
    {
        this.transform.position=Vector3.MoveTowards(this.transform.position,this.TargetPos,Speed);   
    }
    public void Set(Quaternion Rotation,Vector3 InitialPos,Vector3 TargetPos,float Damage,float Speed,int HitNum,GameObject HitEffect,float EffectDuration)
    {
        this.transform.rotation=Rotation;
        this.transform.position=InitialPos;
        this.TargetPos=TargetPos;
        this.Damage=Damage;
        this.Speed=Speed;
        this.HitNum=HitNum;
        this.HitEffect=HitEffect;      
        this.EffectDuration=EffectDuration;
    }
    public void SetRef(Queue<GameObject> EffectQueueRef,Queue<GameObject> BulletQueueRef)
    {
        this.EffectQueueRef=EffectQueueRef;
        this.BulletQueueRef=BulletQueueRef;
    }
    public void SetTargetPos(Vector3 Position)
    {
        TargetPos=Position;
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag=="Monster")
        {
            other.gameObject.GetComponent<MonsterInfo>().DealDamage(Damage);
            HitNum--;
        }
        if(other.tag!="DyingMonster")
        {
            HitEffect.transform.position=this.transform.position;
            HitEffect.SetActive(true);
            HitEffect.GetComponent<BulletEffects>().DeactiveAfterTime(EffectQueueRef,EffectDuration);
        }
        if(HitNum==0||other.transform.tag=="Obstacles")
        {
            BulletQueueRef.Enqueue(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }
    private void OnDisable() {
        this.gameObject.SetActive(false);
    }
    private void Reset()
    {
        BulletQueueRef.Enqueue(this.gameObject);
        this.gameObject.SetActive(false);
    }
}
