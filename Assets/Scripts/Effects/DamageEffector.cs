using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class DamageEffector : MonoBehaviour
{
    [Serializable]private enum EDamageType
    {  
        Single,
        Continuous
    }
    [Serializable]private enum EHitType
    {
        MonsterOnly,
        PlayerAndMonster
    }

    [SerializeField]private EDamageType DamageType;
    [SerializeField]private EHitType HitType;
    [SerializeField]private float DamageRatio;
    [SerializeField]private float HitInterval;
    // Start is called before the first frame update
    private bool DamagableStatus;

    private List<GameObject> DamageController;
    void Start()
    {
        DamagableStatus=true;
        if(DamageType==EDamageType.Continuous)InvokeRepeating("DealDamage",HitInterval,HitInterval);
    }

    public void DealDamage()
    {
        for(int i=0;i<DamageController.Count;i++)
        {
            if(DamageController[i]!=null)
            {
                if(DamageController[i].GetComponent<MonsterInfo>()==true)
                {
                    if(DamageController[i].activeSelf==true)
                        DamageController[i].GetComponent<MonsterInfo>().DealDamage(DamageRatio*CharacterInfo.Instance.GetDamage());

                }
            }
        }
    }
    private void OnTriggerEnter(Collider other) 
    {

        if(other.CompareTag("Monster"))
        {
            other.GetComponent<MonsterInfo>().DealDamage(CharacterInfo.Instance.GetDamage()*DamageRatio);
            DamageController.Add(other.gameObject);
        }

    }
    private void OnTriggerExit(Collider other) 
    {

         DamageController.Remove(other.gameObject);
    
    }
    private void OnEnable() {
           DamageController=new List<GameObject>();
    }
}
