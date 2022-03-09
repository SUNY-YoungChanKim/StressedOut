using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageToCharacter : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            if(this.transform.parent.GetComponent<MonsterInfo>()!=null)
                CharacterInfo.Instance.DealDamage(this.transform.parent.GetComponent<MonsterInfo>().GetDamage());
            else if(this.transform.parent.parent.GetComponent<MonsterInfo>()!=null)
                 CharacterInfo.Instance.DealDamage(this.transform.parent.parent.GetComponent<MonsterInfo>().GetDamage());
        }
    }
}
