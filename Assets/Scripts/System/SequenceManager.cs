using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SequenceManager : MonoBehaviour
{
    [System.Serializable]
    private class MonsterStruct
    {
        [SerializeField]private GameObject MonsterPrefab;
        [SerializeField]private float CreateionStartTimeAsSeconds;
        [SerializeField]private float CreationInterval;
        [SerializeField]private int CreationNumInInterval;

        private float CurrentTimeGenerated;
        private Queue<GameObject> ObjectQueue=new Queue<GameObject>();

        public Queue<GameObject> GetObjectQueue(){return ObjectQueue;}
        public void SetObjectQueue(Queue<GameObject> ObjectQueue){this.ObjectQueue=ObjectQueue;}
        public float GetCreateionStartTimeAsSeconds(){return CreateionStartTimeAsSeconds;}
        public float GetCreationInterval(){return CreationInterval;}
        public int GetCreationNumInInterval(){return CreationNumInInterval;}
        public GameObject GetMonsterPrefab(){return MonsterPrefab;}

        public float GetCurrentTimeGenerated(){return CurrentTimeGenerated;}
        public void SetCurrentTimeGenerated(float CurrentTimeGenerated){this.CurrentTimeGenerated=CurrentTimeGenerated;}
    }
    [Header("Time")]
    [SerializeField]private float GameTimeLimitAsSec;


    [Header("BossCreation")]
    [SerializeField]private List<GameObject> BossList;
    [SerializeField]private float BossHealthRatio=1;
    [SerializeField]private int ChestNum=1;
    private float BossCreateInterval=120.0f;


    [Header("MonsterCreation")]
    [SerializeField]private List<MonsterStruct> MonsterList;

    [SerializeField]private float MinMonsterCreationDistance;


    [SerializeField]private float MonsterCreationRatio=1.0f;

    [SerializeField]private float MonsterHealthRatio=1.0f;
    [SerializeField]private float EXPRatio=1;

    private float time_start;
    private float time_current;
    private bool Paused=false;

    private Transform MonsterManager;
    static SequenceManager instance=null;

    public bool Playing=true;
    public static SequenceManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        instance=this;
        Reset_Timer();

    }
    void Start()
    {

        MonsterManager=GameObject.Find("MonsterManager").transform;
        InvokeRepeating("CreateBossSeq",BossCreateInterval,BossCreateInterval);
        InvokeRepeating("IncreaseScoreSecond",1.0f,1.0f);
        Invoke("GameoverSeq",GameTimeLimitAsSec);
    }

    // Update is called once per frame
    void Update()
    {
        Check_Timer();
        CreateMonsterSeq();
    }

    public void IncreaseScoreSecond()
    {
        Score.Instance.IncreaseScore(10);
    }

    public void IncreaseScore(int Value)
    {
        Score.Instance.IncreaseScore(Value);
    }
    private void Check_Timer()
    {
        time_current = Time.time - time_start;
    }
    private void Reset_Timer()
    {
        time_start = Time.time;
        time_current = 0;
    }
    public float GetLeftTime()
    {
        return GameTimeLimitAsSec-time_current;
    }
    private Vector3 CaculateGenerablePos()
    {
        float BordSize=75.0f;
        Vector3 pos = CharacterInfo.Instance.transform.position;
        float GenerableXLeft,GenerableXRight;
        float GenerableUp,GenerableDown;



        GenerableXLeft=BordSize+pos.x;
        GenerableXRight=BordSize-pos.x;
        GenerableUp=BordSize-pos.z;
        GenerableDown=BordSize+pos.z;

        pos.x=GenerableXLeft>=GenerableXRight?
        Random.Range(MinMonsterCreationDistance,GenerableXLeft)*-1:
        Random.Range(MinMonsterCreationDistance,GenerableXRight);

        if(GenerableXLeft<MinMonsterCreationDistance)
            pos.x= Random.Range(MinMonsterCreationDistance,GenerableXRight);
        else if(GenerableXRight<MinMonsterCreationDistance)
            pos.x=Random.Range(MinMonsterCreationDistance,GenerableXLeft)*-1;
        else
            pos.x=Random.Range(0,2)==0?Random.Range(MinMonsterCreationDistance,GenerableXLeft)*-1: Random.Range(MinMonsterCreationDistance,GenerableXRight);



        if(GenerableUp<MinMonsterCreationDistance)
            pos.z= Random.Range(MinMonsterCreationDistance,GenerableDown)*-1;
        else if(GenerableDown<MinMonsterCreationDistance)
            pos.z= Random.Range(MinMonsterCreationDistance,GenerableUp);
        else
            pos.z=Random.Range(0,2)==0?Random.Range(MinMonsterCreationDistance,GenerableUp):Random.Range(MinMonsterCreationDistance,GenerableDown)*-1;

        return pos;
    }
    public void LevelUPSequence(int Value)
    {
        Pause();
        CardSystem.Instance.SequenceStart(Value);
        
    }
    public void Pause()
    {
        Time.timeScale=0;
        VirtualPadCanvas.Instance.Deactivate();
        Paused=true;
    }
    public void Resume()
    {
        Time.timeScale=1.0f;
        VirtualPadCanvas.Instance.Activate();
        Paused=false;
    }
    public void MultiplyMonsterCreationRatio(float Value)
    {
        MonsterCreationRatio*=Value;
    }
    private void CreateBossSeq()
    {
        if(BossList.Count>0)
        {
            GameObject t= Instantiate(BossList[0],CaculateGenerablePos()+new Vector3(0,5,0),Quaternion.identity);
            t.GetComponent<MonsterInfo>().SetHealthRatio(BossHealthRatio);
            BossList.Remove(BossList[0]);
        }
    }
    public void SetBossHealthRatio(float Value)
    {
        BossHealthRatio=Value;
    }
    public void SetMonsterHealthRatio(float Value)
    {
        MonsterHealthRatio=Value;
    }
    public void IncreaseChestNum(int Value)
    {
        ChestNum+=Value;
    }
    public float GetBossHealthRatio()
    {
        return BossHealthRatio;
    }
    public float GetMonsterHealthRatio()
    {
        return MonsterHealthRatio;
    }
    public float GetEXPRatio()
    {
        return EXPRatio;
    }
    public int GetChestNum()
    {
        return ChestNum;
    }
    public void SetEXPRatio(float Value)
    {
        EXPRatio=Value;
    }
    public void GameoverSeq()
    {
        Time.timeScale=0;
        Playing=false;
        UIUpdator.Instance.PlayGameOver();
        StartCoroutine("MoveToScoreScene");
    }
    IEnumerator  MoveToScoreScene()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        SceneManager.LoadScene("Score");
    }
    private void CreateMonsterSeq()
    {
        GameObject Temp;
    
        for(int i=0;i<MonsterList.Count;i++)
        {
            if(MonsterList[i].GetCreateionStartTimeAsSeconds()<=time_current&&
                MonsterList[i].GetCurrentTimeGenerated()+MonsterList[i].GetCreationInterval()<=time_current)
            {
                for(int j=0;j<MonsterList[i].GetCreationNumInInterval()*MonsterCreationRatio;j++)
                {

                    if(MonsterList[i].GetObjectQueue().Count==0)
                    {
                        Temp=Instantiate(MonsterList[i].GetMonsterPrefab());
                        Temp.transform.parent=MonsterManager;
                    }
                    else
                    {
                        Temp=MonsterList[i].GetObjectQueue().Dequeue();
    
                    }
                    
               
                    
                    Temp.GetComponent<MonsterInfo>().Set(CaculateGenerablePos(),time_current,MonsterList[i].GetObjectQueue());
                    Temp.GetComponent<MonsterInfo>().SetHealthRatio(MonsterHealthRatio);
                    MonsterList[i].SetCurrentTimeGenerated(time_current);

                }
            }
        }
    }
}
