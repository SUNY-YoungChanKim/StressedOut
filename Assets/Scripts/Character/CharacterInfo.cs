using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    static CharacterInfo instance=null;
    [Header ("Player Status")]
    [SerializeField]private int Money=0;
    [SerializeField]private float Speed=0.15f;
    [SerializeField]private float FireRate=1.0f;
    [Range(1.0f,5.0f)]
    [SerializeField]private float Damage=1.0f;
    [SerializeField]private int ProjectileNum=1;

    [SerializeField]private float MaxEXP;
    [SerializeField]private float EXPIncreaseRatio;
    private float CurrentEXP;

    private int LV=1;

    [SerializeField]private float MAXHealth;
    private float CurrentHealth;

    [Header("Sockets")]
    [SerializeField]private Transform WeaponSocekt;

    [Header("Weapons")]
    [SerializeField]private List<GameObject> WeaponList;

    
    // Start is called before the first frame update
    public static CharacterInfo Instance
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
    }
    void Start()
    {
     CurrentHealth=MAXHealth;   
    }

    // Update is called once per frame
    void Update()
    {

    }
    public Vector3 GetPos() {return this.transform.position;}
    public float GetSpeed(){return Speed;}
    public float GetFireRate(){return FireRate;}
    public float GetDamage(){return Damage;}
    public float GetMaxHelath(){return MAXHealth;}
    public float GetCurrentHealth(){return CurrentHealth;}
    public int GetLV(){return LV;}
    public void IncreaseEXP(float Value)
    {
        CurrentEXP+=Value*SequenceManager.Instance.GetEXPRatio();
        LevelUPCheck();
    }

    public float GetMaxEXP(){return MaxEXP;}
    public float GetCurrentEXP(){return CurrentEXP;}

    public int GetMoney(){return Money;}
    public void IncreaseMoney(int Value){Money+=Value;}

    public int GetProjectileNum(){return ProjectileNum;}

    public void SetCurrentHealth(float Value)
    {
        CurrentHealth=Value;

    }
    public int AddWeapon(GameObject Weapon)
    {
        WeaponList.Add(Weapon);
        return WeaponList.Count-1;
    }
    public void IncreaseHP(float Value)
    {
        MAXHealth+=Value;
        CurrentHealth+=Value;
    }
    public void IncreaseSpeed(float Value)
    {
        Speed=Value;
    }
    public void IncreaseFireRate(float Value)
    {
        FireRate=Value;

        for(int i=0;i<WeaponList.Count;i++)WeaponList[i].GetComponent<Weapon>().ReCaculateFire();
    }
    public void IncreaseDamage(float Value)
    {
        Damage=Value;
    }
    public void IncreaseProjectileNum(int Value)
    {
        ProjectileNum+=Value;
    }

    public void LevelUPCheck()
    {
        int LevelUp=0;
        while((int)(CurrentEXP/MaxEXP)!=0)
        {
            CurrentEXP-=MaxEXP;
            MaxEXP*=EXPIncreaseRatio;
            LevelUp++;
        }
        if(LevelUp!=0)
        {
            LV+=LevelUp;
            SequenceManager.Instance.LevelUPSequence(LevelUp);
        }

    }

    public void DealDamage(float Damage)
    {
        this.GetComponent<AudioSource>().Play();
        UIUpdator.Instance.PlayCrackAnim();
        CurrentHealth=CurrentHealth-Damage>=0?CurrentHealth-Damage:0;
        if(CurrentHealth<=0)SequenceManager.Instance.GameoverSeq();
    }
    public Vector3 GetWeaponSocketLocation() {return WeaponSocekt.position;}
}
