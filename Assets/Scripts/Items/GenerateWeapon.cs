using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateWeapon : MonoBehaviour
{
    [SerializeField]int Num;
    // Start is called before the first frame update
    public void Call()
    {
        for(int i=0;i<Num;i++)Instantiate(GenerableItemList.Instance.PopWeapons(),CharacterInfo.Instance.transform);
    }
}
