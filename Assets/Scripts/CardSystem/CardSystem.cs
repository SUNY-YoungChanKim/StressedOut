using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : MonoBehaviour
{

    static private CardSystem instance;

    [SerializeField]private GameObject CardCollection;

    [Header("DeckList")]
    [SerializeField]private List<GameObject> RedCardPrefabs;
    [SerializeField]private List<GameObject> YellowCardPrefabs;
    [SerializeField]private List<GameObject> GreenCardPrefabs;

    [Header("LayOut")]
    [SerializeField]private GameObject BackGround;
    [SerializeField]private float Padding;
    private int NumberOfDraw;

    private List<GameObject> RedCardList=new List<GameObject>(),YellowCardList=new List<GameObject>(),GreenCardList=new List<GameObject>();
    private GameObject RedCardPick,YellowCardPick,GreenCardPick;

    public static CardSystem Instance
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
        GameObject temp;
        for(int i=0;i<RedCardPrefabs.Count;i++)
        {
            temp=Instantiate(RedCardPrefabs[i],CardCollection.transform);
            temp.GetComponent<RectTransform>().Translate(-Padding,0,0);
            temp.SetActive(false);
            RedCardList.Add(temp);
        }
        for(int i=0;i<YellowCardPrefabs.Count;i++)
        {
            temp=Instantiate(YellowCardPrefabs[i],CardCollection.transform);
            temp.SetActive(false);
            YellowCardList.Add(temp);
        }
        for(int i=0;i<GreenCardPrefabs.Count;i++)
        {
            temp=Instantiate(GreenCardPrefabs[i],CardCollection.transform);
            temp.GetComponent<RectTransform>().Translate(Padding,0,0);
            temp.SetActive(false);
            GreenCardList.Add(temp);
        }
    }

    // Update is called once per frame

    private void Update() 
    {
        if(SequenceManager.Instance.Playing==false)
        {
            this.gameObject.SetActive(false);
        }
    }
    public void SequenceStart(int Number)
    {
        NumberOfDraw=Number;
        BackGround.SetActive(true);
        SequenceMove();
    }
    public void RemoveCard(GameObject Target)
    {
        
        RedCardList.RemoveAll(x=>x.name==Target.name);
        YellowCardList.RemoveAll(x=>x.name==Target.name);
        GreenCardList.RemoveAll(x=>x.name==Target.name);
    }
    private void Pick()
    {

        if(RedCardList.Count>0)
        {
            RedCardPick=RedCardList[Random.Range(0,RedCardList.Count)];
            RedCardPick.SetActive(true);
        }
        if(YellowCardList.Count>0)
        {
            YellowCardPick=YellowCardList[Random.Range(0,YellowCardList.Count)];
            YellowCardPick.SetActive(true);
        }
        if(GreenCardList.Count>0)
        {
            GreenCardPick=GreenCardList[Random.Range(0,GreenCardList.Count)];
            GreenCardPick.SetActive(true);
        }


 

        
    }
    private void Blow()
    {
        if(RedCardPick!=null)RedCardPick.SetActive(false);
        if(YellowCardPick!=null)YellowCardPick.SetActive(false);
        if(GreenCardPick!=null)GreenCardPick.SetActive(false);
    }
    public void SequenceMove()
    {
        Blow();
        if(NumberOfDraw==0)
        {
            SequenceManager.Instance.Resume();
            BackGround.SetActive(false);
        }
        else
        {
            NumberOfDraw--;
            Pick();
        }
    }

}
