using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LengthBomb : MonoBehaviour
{
    [SerializeField]private float distance;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(this.transform.position,CharacterInfo.Instance.transform.position)<=distance&&this.gameObject.CompareTag("Monster"))
        {
            this.GetComponent<MonsterInfo>().Die();
        }
    }
}
