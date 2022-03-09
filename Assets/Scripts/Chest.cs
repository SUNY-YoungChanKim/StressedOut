using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    // Start is called before the first frame update
    private bool Opened=false;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player")&&Opened==false)
        {
            this.GetComponent<Animator>().Play("Open");
            for(int i=0;i<SequenceManager.Instance.GetChestNum();i++)
            Instantiate(GenerableItemList.Instance.Pop(),CharacterInfo.Instance.transform);
            Opened=true;
            Destroy(this.gameObject);
        }
    }
}
