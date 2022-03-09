using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Firebase;
using Firebase.Database;
using Firebase.Extensions;
public class DBAccesor : MonoBehaviour
{ 
    [System.Serializable]
    public class RankingBoard
    {
        int Score;
        string Name;
        public RankingBoard(string Name,int Score)
        {
            this.Score=Score;
            this.Name=Name;
        }
        public RankingBoard(string Name,string Score)
        {
            this.Score=int.Parse(Score);
            this.Name=Name;
        }
        public int GetScore()
        {
            return Score;
        }
        public string GetName()
        {
            return Name;
        }
        public int CompareTo(RankingBoard Target)
        {
            if(Target.GetScore()>this.GetScore())return 1;
            else if(Target.GetScore()<this.GetScore())return -1;
            return 0;
        }

    }


    DatabaseReference Reference=null;
    public List<RankingBoard> RankingBoardList=new List<RankingBoard>();
    RankingBoard MyScore;
    private static DBAccesor instance;
    public static DBAccesor Instance
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
    void Awake()
    {
        instance=this;
        Time.timeScale=1.0f;

    }
    void Start()
    {
        InterNetCheck();
    }
    public void InterNetCheck()
    {
       
        if(Application.internetReachability==NetworkReachability.NotReachable)
        {
            ResultUIUpdator.Instance.OpenNetWorkPopUp();
        }
        else if(Application.internetReachability==NetworkReachability.ReachableViaCarrierDataNetwork||
        Application.internetReachability==NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            UpdateCheck();

        }
    }
    public async void UpdateCheck()
    {
        await Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
        var dependencyStatus = task.Result;
        if (dependencyStatus == Firebase.DependencyStatus.Available) {
            // Create and hold a reference to your FirebaseApp,
            // where app is a Firebase.FirebaseApp property of your application class.
            Reference = FirebaseDatabase.DefaultInstance.RootReference;
            ReadUserData();
            // Set a flag here to indicate whether Firebase is ready to use by your app.
        } else {
            UnityEngine.Debug.LogError(System.String.Format(
            "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            // Firebase Unity SDK is not safe to use here.
        }
        });
    }
    public void PrintRankingBoardList()
    {
        foreach(RankingBoard temp in RankingBoardList)
        {
            Debug.Log(temp.GetName()+":"+temp.GetScore());
        }
     
    }
    async void ReadUserData()
    {
        await FirebaseDatabase.DefaultInstance.GetReference("users")
            .GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
                Debug.Log("Error");
                // Handle the error...
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach(DataSnapshot data in snapshot.Children)
                {
                    RankingBoardList.Add(new RankingBoard(data.Key.ToString(),data.Value.ToString()));
                    
                }
            }
            MyScore=new RankingBoard("***",Score.Instance.GetScore());
            RankingBoardList.Add(MyScore);
            Sort();
            UIconnect();
        });

    }
    void Sort()
    {
        RankingBoardList.Sort((x1,x2)=>x1.CompareTo(x2));
        PrintRankingBoardList();
    }
    public void UIconnect()
    {
        int Rank = RankingBoardList.FindIndex(x=>x==MyScore);
        string Above2,Above1,Under1,Under2;

        if(Rank-2>=0)
            Above2= (Rank-1) + "."+RankingBoardList[Rank-2].GetName() + " " + RankingBoardList[Rank-2].GetScore();
        else
            Above2="-----------";
        if(Rank-1>=0)
            Above1= Rank + "."+RankingBoardList[Rank-1].GetName() + " " + RankingBoardList[Rank-1].GetScore();
        else
            Above1="-----------";
        if(Rank+1<RankingBoardList.Count)
            Under1=(Rank+2) + "."+RankingBoardList[Rank+1].GetName() + " " + RankingBoardList[Rank+1].GetScore();
        else
            Under1="-----------";
        if(Rank+2<RankingBoardList.Count)
            Under2=(Rank+3) + "."+RankingBoardList[Rank+2].GetName() + " " + RankingBoardList[Rank+2].GetScore();
        else
            Under2="-----------";

        ResultUIUpdator.Instance.GetRankBoard(Above2,Above1,Under1,Under2,Rank+1,Score.Instance.GetScore());
    }
    public void WriteUserData(string username, int Score)
    {
        RankingBoard Data=new RankingBoard(username,Score);
        string JsonFormat=JsonUtility.ToJson(Data);
        Reference.Child("users").Child(username).SetValueAsync(Score);
    }

    private void OnDestroy() 
    {
    }
}
