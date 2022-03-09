using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIUpdator : MonoBehaviour
{
    private static UIUpdator instance;
    [SerializeField]private Text Timer;
    [SerializeField]private Text TScore;

    [Header("HP")]
    [SerializeField]private Text HPText;
    [SerializeField]private Image HPProgressBar;
    [SerializeField]private float HPProgressBarSpeed;

    [Header("EXP")]
    [SerializeField]private Text EXPText;
    [SerializeField]private Image EXProgressBar;
    [SerializeField]private float EXProgressBarSpeed;
    [SerializeField]private Text LVText;

    [Header("CrackAnim")]
    [SerializeField]private Animator CrackAnimatior;
    private float CurrentMoneyFollowValue;
    [Header("PopUp")]
    [SerializeField]private GameObject PopUpUI;
    [SerializeField]private GameObject Border;
    [SerializeField]private Text ItemTitle;
    [SerializeField]private Text ItemDescription;
    [SerializeField]private Text RankText;

    [SerializeField]private Sprite ARankBorder;
    [SerializeField]private Sprite BRankBorder;
    [SerializeField]private Sprite CRankBorder;
    [SerializeField]private Sprite DRankBorder;
    [SerializeField]private Sprite ERankBorder;

    [Header("Alarm")]
    [SerializeField]private GameObject WarningSign;
    [SerializeField]private GameObject GameOverSign;


    private List<ItemInfo> ItemPopUpList=new List<ItemInfo>();
    private bool ItemPopuping=false;

    private GameObject ItemPopUpRef;
    private CharacterInfo CharacterRef;
    // Start is called before the first frame update

        public static UIUpdator Instance
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
    private void Awake() {
                instance=this;
    }
    void Start()
    {

        CharacterRef=CharacterInfo.Instance;
    }

    // Update is called once per frame
    private void Update() 
    {
        if(ItemPopuping==false&&ItemPopUpList.Count>0)
        {
            StartCoroutine("Popup");
        }

        Timer.text=SecToTime(SequenceManager.Instance.GetLeftTime());
        HPText.text=(int)CharacterRef.GetCurrentHealth()+"/"+(int)CharacterRef.GetMaxHelath();
        HPProgressBar.fillAmount= Mathf.SmoothStep(HPProgressBar.fillAmount,
        (CharacterRef.GetCurrentHealth()/CharacterRef.GetMaxHelath()),
        HPProgressBarSpeed);


        EXPText.text = EXPtoTIme(CharacterRef.GetMaxEXP(),CharacterRef.GetCurrentEXP());
        EXProgressBar.fillAmount=Mathf.SmoothStep(EXProgressBar.fillAmount,
        (CharacterRef.GetCurrentEXP()/CharacterRef.GetMaxEXP()),
        EXProgressBarSpeed);


        LVText.text="LV."+CharacterRef.GetLV().ToString();

        TScore.GetComponent<Text>().text="Score:"+Score.Instance.GetScore();

    }

    public void PopUpListIn(ItemInfo Info)
    {
        ItemPopUpList.Add(Info);
    }
    IEnumerator Popup()
    {
        ItemPopuping=true;
        ItemTitle.text =ItemPopUpList[0].GetName();
        ItemDescription.text=ItemPopUpList[0].GetDescription();
        ItemPopUpRef= Instantiate(ItemPopUpList[0].GetImagePrefab(),Border.transform);

        switch(ItemPopUpList[0].GetRarity())
        {
            case ItemInfo.ERarity.A:
            Border.GetComponent<Image>().sprite=ARankBorder;
            RankText.text="A";
            RankText.color=new Color(255,80,80);
            break;
            case ItemInfo.ERarity.B:
            Border.GetComponent<Image>().sprite=BRankBorder;
            RankText.text="B";
            RankText.color=new Color(255,255,80);
            
            break;
            case ItemInfo.ERarity.C:
            Border.GetComponent<Image>().sprite=CRankBorder;
            RankText.text="C";
            RankText.color=new Color(80,80,255);
            break;
            case ItemInfo.ERarity.D:
            Border.GetComponent<Image>().sprite=DRankBorder;
            RankText.color=new Color(80,255,80);
            RankText.text="D";
            break;
            case ItemInfo.ERarity.E:
            Border.GetComponent<Image>().sprite=ERankBorder;
            RankText.color=new Color(255,160,80);
            RankText.text="E";
            break;
        }

        for(int i=0;i<25;i++)
        {
            PopUpUI.GetComponent<RectTransform>().localPosition-=new Vector3(PopUpUI.GetComponent<RectTransform>().sizeDelta.x/25,0,0);
            yield return new WaitForSeconds(0.01f);
        }
        PopUpUI.GetComponent<AudioSource>().Play();

        for(int i=0;i<2;i++)yield return new WaitForSeconds(1.0f);

        StartCoroutine("PopDown");

    }
    IEnumerator PopDown()
    {

        for(int i=0;i<25;i++)
        {
            PopUpUI.GetComponent<RectTransform>().localPosition+=new Vector3(PopUpUI.GetComponent<RectTransform>().sizeDelta.x/25,0,0);
            yield return new WaitForSeconds(0.01f);
        }
        ItemPopUpList.Remove(ItemPopUpList[0]);
        ItemPopuping=false;
        Destroy(ItemPopUpRef);
    }
    private string SecToTime(float TimeSec)
    {
        int Minute=(int)TimeSec/60;
        int Second=(int)TimeSec%60;

        string SMinute=Minute<10 ? "0"+Minute:Minute.ToString();
        string SSecond=Second<10 ? "0"+Second:Second.ToString();

        return SMinute+":"+SSecond;
    }
    private string EXPtoTIme(float MaxEXP,float CurrentEXP)
    {
        return (int)CurrentEXP+"/"+(int)MaxEXP;
    }

    public void PlayCrackAnim()
    {
        CrackAnimatior.SetTrigger("Play");
    }

    public void PlayAlarm()
    {
        WarningSign.GetComponent<AudioSource>().Play();
        WarningSign.GetComponent<Animator>().SetTrigger("Play");
    }
    public void PlayGameOver()
    {
        GameOverSign.GetComponent<Animator>().SetTrigger("Play");
        GameOverSign.GetComponent<AudioSource>().Play();
    }
}
