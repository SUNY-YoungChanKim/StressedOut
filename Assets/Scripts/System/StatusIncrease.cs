using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class StatusIncrease : MonoBehaviour
{
    [Serializable]
    private enum EStatType
    {
        HP,
        Money,
        Speed,
        FireRate,
        Damage,
        ProjectileNum,
    }
    [SerializeField]private EStatType StatType;
    [SerializeField]private float IncreaseRate;
    // Start is called before the first frame update
    void Start()
    {
        switch(StatType)
        {
            case EStatType.HP:
                CharacterInfo.Instance.IncreaseHP(IncreaseRate);
                break;
            case EStatType.Money:
                CharacterInfo.Instance.IncreaseMoney((int)IncreaseRate);
                break;
            case EStatType.Speed:
                CharacterInfo.Instance.IncreaseSpeed(CharacterInfo.Instance.GetSpeed()*IncreaseRate);
                break;
            case EStatType.FireRate:
                CharacterInfo.Instance.IncreaseFireRate(CharacterInfo.Instance.GetFireRate()*IncreaseRate);
                break;
            case EStatType.Damage:
                CharacterInfo.Instance.IncreaseDamage(CharacterInfo.Instance.GetDamage()*IncreaseRate);
                break;
            case EStatType.ProjectileNum:
                CharacterInfo.Instance.IncreaseProjectileNum((int)IncreaseRate);
                break;
        }  

    }

    // Update is called once per frame
}
