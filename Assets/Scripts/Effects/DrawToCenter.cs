using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawToCenter : MonoBehaviour
{
    [SerializeField]private List<GameObject> EffectedMonsters;
    [SerializeField]private float Power;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable() 
    {
        EffectedMonsters=new List<GameObject>();
    }
    private void FixedUpdate()
    {
        for(int i=0;i<EffectedMonsters.Count;i++)
        {
            if(EffectedMonsters[i]!=null)
            {
            EffectedMonsters[i].transform.localPosition=Vector3.MoveTowards(EffectedMonsters[i].transform.position,this.transform.position,Power);
            }
        }
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Monster"))
        {
            EffectedMonsters.Add(other.transform.gameObject);
        }    
    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Monster"))
        {
            EffectedMonsters.Remove(other.transform.gameObject);
        }    
    }
}
