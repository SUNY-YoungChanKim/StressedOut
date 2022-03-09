using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class FadeOut : MonoBehaviour
{
    // Start is called before the first frame update


    void Start()
    {
        if(this.GetComponent<Image>()!=null)
                this.GetComponent<Image>().color=new Color(this.GetComponent<Image>().color.r,this.GetComponent<Image>().color.g,this.GetComponent<Image>().color.b,0);
        if(this.GetComponent<Text>()!=null)
                this.GetComponent<Text>().color=new Color(this.GetComponent<Text>().color.r,this.GetComponent<Text>().color.g,this.GetComponent<Text>().color.b,0);
        StartCoroutine("AlphaIncrease");
    }

    IEnumerator AlphaIncrease()
    {
        for(int i=0;i<100;i++)
        {
            Debug.Log(i);
            if(this.GetComponent<Image>()!=null)
                this.GetComponent<Image>().color=new Color(this.GetComponent<Image>().color.r,this.GetComponent<Image>().color.g,this.GetComponent<Image>().color.b,1/100.0f*(float)i);
            if(this.GetComponent<Text>()!=null)
                this.GetComponent<Text>().color=new Color(this.GetComponent<Text>().color.r,this.GetComponent<Text>().color.g,this.GetComponent<Text>().color.b,1/100.0f*(float)i);
            
            yield return new WaitForSeconds(0.02f);
        }
    }
}
