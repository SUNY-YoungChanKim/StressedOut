using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class MenuHandler : MonoBehaviour
{
    [SerializeField]private GameObject MainMenu;
    [SerializeField]private GameObject SettingMenu;

    [SerializeField]private GameObject BGMSlider;
    [SerializeField]private GameObject MasterSlider;

    [SerializeField]private GameObject MainMenuPopUp;
    [SerializeField]private GameObject ExitPopUp;

    // Start is called before the first frame update

    public AudioMixer ControllingAudioMixer;
 

    private void Start() {
        float Volume;
        ControllingAudioMixer.GetFloat("Master",out Volume);
        MasterSlider.GetComponent<Slider>().value=(Volume+80)/100;
        ControllingAudioMixer.GetFloat("BGM",out Volume);
        BGMSlider.GetComponent<Slider>().value=(Volume+80)/100;
    }
    public void OpenMenu()
    {
        if(SequenceManager.Instance.Playing==true)
        {       
             MainMenu.GetComponent<Animator>().SetTrigger("Open");
        
            Time.timeScale=0;
        }


    }
    public void OpenSettingMenu()
    {
        SettingMenu.GetComponent<Animator>().SetTrigger("Open");
    }
    public void Back()
    {
        SettingMenu.GetComponent<Animator>().SetTrigger("Close");
    }
    public void Close()
    {
        MainMenu.GetComponent<Animator>().SetTrigger("Close");
        Time.timeScale=1.0f;
    }
    public void ChangeVolume()
    {
        ControllingAudioMixer.SetFloat("Master",(MasterSlider.GetComponent<Slider>().value*100)-80);
    }
    public void ChangeBGM()
    {
        ControllingAudioMixer.SetFloat("BGM",(BGMSlider.GetComponent<Slider>().value*100)-80);
    }
    public void PopUpMainMenu()
    {
        MainMenuPopUp.GetComponent<Animator>().SetTrigger("Open");
    }
    public void MainMenuYes()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void MainMenuNo()
    {
        MainMenuPopUp.GetComponent<Animator>().SetTrigger("Close");
    }

    public void PopUpExitMenu()
    {
        ExitPopUp.GetComponent<Animator>().SetTrigger("Open");
    }
    public void ExitYes()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit(); // 어플리케이션 종료
        #endif
    }
    public void ExitNo()
    {
        ExitPopUp.GetComponent<Animator>().SetTrigger("Close");
    }
}
