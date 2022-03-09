using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent (typeof (NavMeshAgent))]
public class MonsterInfo : MonoBehaviour
{
    
    [Header("Status")]
    [SerializeField]private float InitialHealth;
    [SerializeField]private float Damage;
    [SerializeField]private float EXP;
    [SerializeField]private int Score;


    [Header("DyingEffect")]
    [SerializeField]private GameObject DyingEffectPrefab;
    [SerializeField]private float DyingEffectDuration;

    private NavMeshAgent AI;
    public float Health;
    private float MoveSpeed;
    private GameObject DyingEffect;
    private Animator AnimationController;

    private Queue<GameObject> QueueRef;
    // Start is called before the first frame update
    void Start()
    {
        AI=this.GetComponent<NavMeshAgent>();

        AnimationController=this.gameObject.transform.Find("Animator").GetComponent<Animator>();
        DyingEffect=Instantiate(DyingEffectPrefab);
        DyingEffect.transform.parent=this.transform;
        DyingEffect.transform.position=this.transform.localPosition;
        DyingEffect.SetActive(false);

        Health=InitialHealth;


    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Init()
    {
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag=="Player"&&this.tag!="DyingMonster")
        {
            CharacterInfo.Instance.DealDamage(Damage);
            Die();
        }    
    }
    public void DealDamage(float Damage)
    {
        Health-=Damage;
        if(Health<=0)Die();
        else AnimationController.SetTrigger("Hit");
    }
    public void Die()
    {
        SequenceManager.Instance.IncreaseScore(Score);

        CharacterInfo.Instance.IncreaseEXP(EXP);
        AI.isStopped=true;
        AnimationController.SetTrigger("Die");
       
    
        this.gameObject.tag="DyingMonster";
        DyingEffect.SetActive(true);

        Invoke("Reset",DyingEffectDuration);
    }
    private void Reset()
    {
        if(QueueRef!=null)
        {
            this.gameObject.SetActive(false);
            QueueRef.Enqueue(this.gameObject);
            this.tag="Monster";
        }
        else
            Destroy(this.gameObject);
    }
    public float GetDamage()
    {
        return Damage;
    }
    public float GetInitialHealth()
    {
        return InitialHealth;
    }
    public float GetCurrentHealth()
    {
        return Health;
    }
    public void SetHealthRatio(float Value)
    {
        InitialHealth*=Value;
        Health*=Value;
    }
    public void Set(Vector3 InitialPos,float time_current,Queue<GameObject> QueueRef)
    {
        this.gameObject.layer=3;
        this.gameObject.tag="Monster";
        this.transform.position=InitialPos;
        this.Health=InitialHealth+ (InitialHealth*(time_current)/60);
        this.QueueRef = QueueRef;
        this.gameObject.SetActive(true);

        if(DyingEffect!=null)
                DyingEffect.SetActive(false);
        
    }
}
