using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateFocusEvent : MonoBehaviour
{
    [SerializeField]private float Speed,WaitTime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("EventCall",1.0f);
        
    }
    private void EventCall()
    {
        UIUpdator.Instance.PlayAlarm();
        MainCameraHandler.Instance.CallMoveEnvet(this.transform.position,Speed,WaitTime);
    }
    // Update is called once per frame
}
