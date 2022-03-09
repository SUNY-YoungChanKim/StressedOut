using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]private float Speed;
    private Vector3 TargetPos;
    private float Damage;

    // Start is called before the first frame update

    public void Set(Vector3 TargetPos,float Damage)
    {
        this.TargetPos=TargetPos;
        this.Damage=Damage;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate() {
        this.transform.position=Vector3.MoveTowards(this.transform.position,TargetPos,Speed);
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<CharacterInfo>().DealDamage(Damage);
        }
        Destroy(this.gameObject);
    }
}
