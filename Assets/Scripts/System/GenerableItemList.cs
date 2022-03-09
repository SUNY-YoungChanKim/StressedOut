using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerableItemList : MonoBehaviour
{
    private static GenerableItemList instance;
    [SerializeField]private List<GameObject> ItemList;

    public static GenerableItemList Instance
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
    }

    // Update is called once per frame

    public GameObject PopWeapons()
    {
        List<GameObject> Condition=ItemList.FindAll(x=>x.GetComponent<ItemInfo>().IsitActive());
        GameObject Temp=Condition[Random.Range(0,Condition.Count)];
        ItemList.Remove(Temp);
        return Temp;
    }
    public GameObject Pop()
    {
        GameObject Temp=ItemList[Random.Range(0,ItemList.Count)];
        if(Temp.GetComponent<ItemInfo>().IsitActive())ItemList.Remove(Temp);
        return Temp;
    }

}
