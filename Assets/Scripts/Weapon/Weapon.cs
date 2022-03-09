using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : MonoBehaviour
{
    private enum SWeaponType
    {
        Circle,
        Fan,
        Sparkle
    }
    [Header("Bullet")]
    [SerializeField]private GameObject BulletPrefab;
    [SerializeField]private int HittableNumber;
    private Queue<GameObject> BulletQueue=new Queue<GameObject>();

    [Header("HitEffect")]
    [SerializeField]private GameObject HitEffectPrefab;
    [SerializeField]private float EffectDuration;

    private Queue<GameObject> HitEffectQueue=new Queue<GameObject>();

    [Header ("Weapon Status")]
    [SerializeField]private float SpeedRatio;
    [SerializeField]private float DamageRatio;

    [SerializeField]private float FireRate;

    [SerializeField]private SWeaponType WeaponType;

    private GameObject ObjectPoolManager;
    private int ID;

    // Start is called before the first frame update
    void Start()
    {
        ID=CharacterInfo.Instance.AddWeapon(this.gameObject);
        InvokeRepeating("Fire",0,CharacterInfo.Instance.GetFireRate()*FireRate);
        ObjectPoolManager= GameObject.Find("ObjectPoolManager");
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void ReCaculateFire()
    {
        CancelInvoke("Fire");
        InvokeRepeating("Fire",CharacterInfo.Instance.GetFireRate()*FireRate,CharacterInfo.Instance.GetFireRate()*FireRate);
    }
    public void Fire()
    {
        List<Vector3> TargetPos=null;

        switch(WeaponType)
        {
            case SWeaponType.Fan:
                TargetPos= GetFanVectorDirList(CharacterInfo.Instance.GetProjectileNum());
            break;
            case SWeaponType.Circle:
            break;
            case SWeaponType.Sparkle:
            break;
        }




        for(int i=0;i<CharacterInfo.Instance.GetProjectileNum();i++)
        {

            GameObject TempEffect, TempBullet;

            if(HitEffectQueue.Count==0)
            {
                TempEffect=Instantiate(HitEffectPrefab);
                TempEffect.transform.parent=ObjectPoolManager.transform;
            }
            else
                TempEffect=HitEffectQueue.Dequeue();
            if(BulletQueue.Count==0)
            {
                TempBullet=Instantiate(BulletPrefab);
                TempBullet.transform.parent=ObjectPoolManager.transform;
            }
            else
                TempBullet=BulletQueue.Dequeue();



            
        

            TempBullet.GetComponent<Bullet>().Set(CharacterInfo.Instance.transform.rotation,
            CharacterInfo.Instance.GetWeaponSocketLocation(),TargetPos[i],
            CharacterInfo.Instance.GetDamage()*DamageRatio,
            CharacterInfo.Instance.GetSpeed()*SpeedRatio,HittableNumber,
            TempEffect,EffectDuration);
            TempBullet.SetActive(true);
            TempEffect.SetActive(false);
            TempBullet.GetComponent<Bullet>().SetRef(HitEffectQueue,BulletQueue);
        }

        if(this.GetComponent<AudioSource>()!=null)this.GetComponent<AudioSource>().Play();
    }

    private List<Vector3> GetFanVectorDirList(int Num)
    {
        List<Vector3> Result = new List<Vector3>();
        float OriginAngle = this.transform.rotation.eulerAngles.y-90;
        float Angle;
        Vector3 Direction;
        
        if(Num%2==1)
        {
            Direction=new Vector3(Mathf.Sin(Mathf.Deg2Rad * OriginAngle),0,Mathf.Cos(Mathf.Deg2Rad * OriginAngle))*1000;
            Result.Add(Direction);
            Num--;
        }
        for(int i=2;i<=Num;i+=2)
        {
                Angle=OriginAngle+(90/(Num/2+1))*(i/2);
                Direction=new Vector3(Mathf.Sin(Mathf.Deg2Rad * Angle),0,Mathf.Cos(Mathf.Deg2Rad * Angle))*1000;
                Result.Add(Direction);
                Angle=OriginAngle-(90/(Num/2+1))*(i/2);
                Direction=new Vector3(Mathf.Sin(Mathf.Deg2Rad * Angle),0,Mathf.Cos(Mathf.Deg2Rad * Angle))*1000;
                Result.Add(Direction);
        }
        return Result; 
    } 
}
