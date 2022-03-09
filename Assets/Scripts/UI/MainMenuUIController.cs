using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuUIController : MonoBehaviour
{
    [SerializeField]private GameObject CreditPopup;
    // Start is called before the first frame update
    public void OpenCredit()
    {
        CreditPopup.GetComponent<Animator>().SetTrigger("Play");
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Loading");
    }
}
