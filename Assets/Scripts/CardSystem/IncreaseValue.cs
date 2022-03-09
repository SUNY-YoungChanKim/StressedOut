using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class IncreaseValue : MonoBehaviour
{
    [Serializable]
    private enum TypeOfIncrease
    {
        MonsterNumber,
        MonsterHealthRatio,
        BossHealthRatio
    }
    [SerializeField]private TypeOfIncrease IncreaseType;
    [SerializeField]private float Value;
    [SerializeField]private int AdditionalValueForChest;
    [SerializeField]private float AdditionalValueForEXPratio;
    // Start is called before the first frame update
    public void Activate()
    {
        switch(IncreaseType)
        {

            case TypeOfIncrease.MonsterNumber:
                SequenceManager.Instance.MultiplyMonsterCreationRatio(Value);
                break;
            case TypeOfIncrease.MonsterHealthRatio:
                SequenceManager.Instance.SetMonsterHealthRatio(SequenceManager.Instance.GetMonsterHealthRatio()*Value);
                SequenceManager.Instance.SetEXPRatio(SequenceManager.Instance.GetEXPRatio()*AdditionalValueForEXPratio);
                break;
            case TypeOfIncrease.BossHealthRatio:
                SequenceManager.Instance.SetBossHealthRatio(SequenceManager.Instance.GetBossHealthRatio()*Value);
                SequenceManager.Instance.IncreaseChestNum(AdditionalValueForChest);
                break;
        }
    }
}
