using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FadeIn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(this.GetComponent<Image>()!=null)
                this.GetComponent<Image>().color=new Color(this.GetComponent<Image>().color.r,this.GetComponent<Image>().color.g,this.GetComponent<Image>().color.b,1);
        if(this.GetComponent<Text>()!=null)
                this.GetComponent<Text>().color=new Color(this.GetComponent<Text>().color.r,this.GetComponent<Text>().color.g,this.GetComponent<Text>().color.b,1);
            
        StartCoroutine("AlphaDecrease");
    }

    // Update is called once per frame
    IEnumerator AlphaDecrease()
    {
        for(int i=0;i<100;i++)
        {
            if(this.GetComponent<Image>()!=null)
                this.GetComponent<Image>().color=new Color(this.GetComponent<Image>().color.r,this.GetComponent<Image>().color.g,this.GetComponent<Image>().color.b,1-1/100.0f*(float)i);
            if(this.GetComponent<Text>()!=null)
                this.GetComponent<Text>().color=new Color(this.GetComponent<Text>().color.r,this.GetComponent<Text>().color.g,this.GetComponent<Text>().color.b,1-1/100.0f*(float)i);
            
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }
}
