using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffects : MonoBehaviour
{
    private Queue<GameObject> QueueRef;
    // Start is called before the first frame update


    public void DeactiveAfterTime(Queue<GameObject> QueueRef,float Time)
    {
        Invoke("Deactive",Time);
        this.QueueRef=QueueRef;
    }
    private void Deactive()
    {
        if(QueueRef!=null)
        {
            this.gameObject.SetActive(false);
            QueueRef.Enqueue(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
}
