using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFollow : MonoBehaviour
{
    [SerializeField]private float Speed;
    private Vector3 Offset;
    // Start is called before the first frame update
    
    private void Awake() 
    {
 
    }
    void Start()
    {
         Offset=this.transform.position-CharacterInfo.Instance.GetPos();   
    }

    // Update is called once per frame
     private void FixedUpdate() 
    {
        
        this.transform.position=Vector3.Lerp(this.transform.position,CharacterInfo.Instance.GetPos()+Offset,Speed*Time.deltaTime);
        
    }
}
