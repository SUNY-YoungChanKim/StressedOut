using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCreate : MonoBehaviour
{
    [SerializeField]private GameObject Chest;
    private void OnDestroy() 
    {
        Instantiate(Chest,this.transform.position,Quaternion.identity);
    }
}
