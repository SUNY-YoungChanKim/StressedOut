using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private static Score instance;
    private int fScore=0;
    // Start is called before the first frame update

    public static Score Instance
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
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        instance=this;
    }
    public void IncreaseScore(int Value)
    {
        fScore+=Value;
    }
    public int GetScore()
    {
        return fScore;
    }
    // Update is called once per frame
}
