using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField]private AudioClip[] PlayList;
    private AudioSource AudioController;
    private int Idx;
    // Start is called before the first frame update
    void Start()
    {
        AudioController=this.GetComponent<AudioSource>();
        ShuffleList(PlayList);
        if(PlayList.Length>0)
        {
            AudioController.clip=PlayList[0];
            AudioController.Play();
        }
        StartCoroutine("AudioCheck");
    }
    private AudioClip[] ShuffleList(AudioClip[] list)
    {
        int random1,  random2;
        AudioClip temp;

        for (int i = 0; i < list.Length; ++i)
        {
            random1 = Random.Range(0, list.Length);
            random2 = Random.Range(0, list.Length);

            temp = list[random1];
            list[random1] = list[random2];
            list[random2] = temp;
        }

        return list;
    }

    // Update is called once per frame
    void Update()
    {
        AudioCheck();
    }
    IEnumerator AudioCheck()
    {
        while(true)
        {

            if(AudioController.isPlaying==false)
            {
                Idx=(Idx+1)%PlayList.Length;
                AudioController.clip=PlayList[Idx];
                AudioController.Play();
            }
            
            yield return new WaitForSecondsRealtime(1.0f);
        }
    }
}
