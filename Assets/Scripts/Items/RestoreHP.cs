using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreHP : MonoBehaviour
{
    [SerializeField]private float Percent;
    // Start is called before the first frame update
    public void Call()
    {
        float RestoreValue=CharacterInfo.Instance.GetMaxHelath()*Percent;

        if(RestoreValue+CharacterInfo.Instance.GetCurrentHealth()>=CharacterInfo.Instance.GetMaxHelath())
            CharacterInfo.Instance.SetCurrentHealth(CharacterInfo.Instance.GetMaxHelath());
        else
        {  
           CharacterInfo.Instance.SetCurrentHealth(CharacterInfo.Instance.GetCurrentHealth()+RestoreValue); 
        }
    }
}
