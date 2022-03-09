using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo : MonoBehaviour
{
    public enum ERarity
    {
        A,B,C,D,E
    }
    public enum EType
    {
        Passive,Active
    }
    [SerializeField]ERarity Rarity;
    [SerializeField]EType TypeInfo;
 
     [SerializeField]GameObject Image;
    [SerializeField]private string Name;
    [SerializeField]private string Description;
    // Start is called before the first frame update

    public ERarity GetRarity()
    {
        return Rarity;
    }

    public EType GetItemType()
    {
        return TypeInfo;
    }
    public string GetName()
    {
        return Name;
    }
    public string GetDescription()
    {
        return Description;
    }

    public GameObject GetImagePrefab()
    {
        return Image;
    }

     private void Start() 
    {
        if(Image!=null)
         UIUpdator.Instance.PopUpListIn(this);
        
    }

    public bool IsitActive()
    {
        return TypeInfo==EType.Active;
    }

}
