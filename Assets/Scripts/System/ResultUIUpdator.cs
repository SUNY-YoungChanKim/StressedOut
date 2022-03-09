using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ResultUIUpdator : MonoBehaviour
{
    [SerializeField]Text Scoretext;
    [SerializeField]Text Rank;

    [SerializeField]Text Above2,Above1,Mine,Under1,Under2;

    [SerializeField]Text InputField;

    [Header("Popup")]
    [SerializeField]private GameObject TextFieldPopUp;

    [SerializeField]private GameObject SaveConfirmPopUp;

    [SerializeField]private GameObject TitleConfirmPopUp;

    [SerializeField]private GameObject NetworkPopUp;

    [Header("Button")]
    [SerializeField]private GameObject SaveButton;


    private static ResultUIUpdator instance;

    private int MyRank;
    private int MyScore;

    // Start is called before the first frame update

    public static ResultUIUpdator Instance
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
    private void Awake() 
    {
        instance=this;
    }
    public void GetRankBoard(string Above2,string Above1,string Under1,string Under2,int tMyrank,int tMyScore)
    {
        this.Above2.text=Above2;
        this.Above1.text=Above1;
        this.Under1.text=Under1;
        this.Under2.text=Under2;
        this.Mine.text=tMyrank+"."+"*** "+tMyScore;

        Scoretext.text=tMyScore.ToString();
        Rank.text=tMyrank.ToString();

        MyRank=tMyrank;
        MyScore=tMyScore;


    }
    // Update is called once per frame

    public void ValidInputCheck()
    {
        if(InputField.text.Length>=4||InputField.text.Length==0)
        {
            TextFieldPopUp.GetComponent<Animator>().SetTrigger("Play");
            Invoke("CloseTextFieldPopUp",1.5f);
        }
        else
        {
            SaveConfirmPopUp.GetComponent<Animator>().SetTrigger("Play");
        }
    }

    public void CloseTextFieldPopUp()
    {
        TextFieldPopUp.GetComponent<Animator>().SetTrigger("Reset");
    }

    public void Save()
    {
        string Name=InputField.text;
        for(int i=0;i<3-Name.Length;i++)Name+=" ";
        DBAccesor.Instance.WriteUserData(Name,MyScore);
        SaveButton.SetActive(false);

        Mine.text=MyRank+"."+Name+" "+MyScore;

        CloseSaveConfirmPopUp();
    }
    public void CloseSaveConfirmPopUp()
    {
        SaveConfirmPopUp.GetComponent<Animator>().SetTrigger("Reset");
    }
    public void OpenTitlePopUp()
    {
        TitleConfirmPopUp.GetComponent<Animator>().SetTrigger("Play");
    }
    public void CloseTitlePopUp()
    {
        TitleConfirmPopUp.GetComponent<Animator>().SetTrigger("Reset");
    }

    public void OpenNetWorkPopUp()
    {
        NetworkPopUp.GetComponent<Animator>().SetTrigger("Play");
    }
    public void Reconnect()
    {
        NetworkPopUp.GetComponent<Animator>().SetTrigger("Reset");
        DBAccesor.Instance.InterNetCheck();
    }
    public void GotoTitle()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
