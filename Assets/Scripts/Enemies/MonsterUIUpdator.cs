using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MonsterUIUpdator : MonoBehaviour
{
    [SerializeField]private GameObject HpBar;
    [SerializeField]private GameObject HPPrgoressbar;
    [SerializeField]private GameObject HPText;
    [SerializeField]private float Speed;
    private MonsterInfo Monster;
    // Start is called before the first frame update
    void Start()
    {
        Monster=transform.parent.GetComponent<MonsterInfo>();
        this.GetComponent<Canvas>().worldCamera=GameObject.Find("UICamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var screenpos=Camera.main.WorldToScreenPoint(transform.parent.transform.position);
        Vector2 localpos=Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(this.GetComponent<RectTransform>(),screenpos,this.GetComponent<Canvas>().worldCamera,out localpos);


        HpBar.transform.localPosition=localpos;
        HPPrgoressbar.transform.localPosition=localpos;
        HPText.transform.localPosition=localpos;
        HPText.GetComponent<Text>().text=(int)Monster.GetCurrentHealth()+"/"+(int)Monster.GetInitialHealth();
        HPPrgoressbar.GetComponent<Image>().fillAmount=Mathf.SmoothStep(HPPrgoressbar.GetComponent<Image>().fillAmount,Monster.GetCurrentHealth()/Monster.GetInitialHealth(),Speed);
    }
}
