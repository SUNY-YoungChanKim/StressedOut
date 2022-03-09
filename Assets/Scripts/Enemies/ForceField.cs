using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    [SerializeField]private float Armor;
    [SerializeField]private int RestoreTime;
    private float CurrentArmor;
    private Vector3 OriginalScale;
    // Start is called before the first frame update
    void Start()
    {
        CurrentArmor=Armor;
        OriginalScale=this.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(CurrentArmor);
        
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Bullet"))
        {
            CurrentArmor-=other.GetComponent<Bullet>().GetDamage();

            if(CurrentArmor<=0)
            {
                StartCoroutine(Destroy());
                StartCoroutine(Restore());

            }
        }    
    }
    IEnumerator Restore()
    {
        this.GetComponent<SphereCollider>().enabled=true;
        for(int i=0;i<RestoreTime;i++)yield return new WaitForSeconds(1.0f);
        for(int i=0;i<=50;i++)
        {
            this.transform.localScale+=OriginalScale/50;
            yield return new WaitForSeconds(0.01f);
        }
        CurrentArmor=Armor;

    }
    IEnumerator Destroy()
    {
        this.GetComponent<SphereCollider>().enabled=false;
        for(int i=1;i<=50;i++)
        {
            Debug.Log("Destory!");
            this.transform.localScale -= OriginalScale/50;
            yield return new WaitForSeconds(0.01f);
        }

    }
}
