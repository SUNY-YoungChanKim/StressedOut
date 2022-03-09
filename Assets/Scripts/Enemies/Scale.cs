using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scale : MonoBehaviour
{
    private Vector3 OriginalSize;
    // Start is called before the first frame update
    void Start()
    {
        OriginalSize=this.transform.localScale;
        StartCoroutine("ScaleDown");
    }

    // Update is called once per frame
    IEnumerator ScaleDown()
    {
        for(int i=0;i<50;i++)
        {
            this.transform.localScale-=OriginalSize/50;
            yield return new WaitForSeconds(0.01f);
        }
        for(int i=0;i<2;i++)yield return new WaitForSeconds(1);
        StartCoroutine("ScaleUp");
    }
    IEnumerator ScaleUp()
    {
        this.GetComponent<AudioSource>().Play();
        for(int i=0;i<50;i++)
        {
            this.transform.localScale+=OriginalSize/50;
            yield return new WaitForSeconds(0.01f);
        }
        for(int i=0;i<2;i++)yield return new WaitForSeconds(1);
         StartCoroutine("ScaleDown");
    }

}
