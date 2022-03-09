using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    [SerializeField]private Transform StartPoint,EndPoint;
    [SerializeField]private float Interval;
    private float[] Speed={1,1.5f,2f,2.5f};
    private float CurrentSpeed;
    private int ReferenceIdx=0;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        Speed=ShuffleArray(Speed);
        CurrentSpeed=1.0f;
        InvokeRepeating("SpeedChange",Interval,Interval);

    }
    private void SpeedChange()
    {
        ReferenceIdx=(ReferenceIdx+1)%4;
    }
    private void Reset()
    {
        this.transform.position=StartPoint.position;
    }
    // Update is called once per frame
    void Update()
    {
        if(this.transform.localPosition==EndPoint.localPosition)Reset();
        if(CurrentSpeed!=Speed[ReferenceIdx])CurrentSpeed+=(Speed[ReferenceIdx]-CurrentSpeed)*Time.deltaTime;
        this.transform.position= Vector3.MoveTowards(this.transform.position,EndPoint.position,CurrentSpeed*Time.deltaTime*5.0f);


    }
    private T[] ShuffleArray<T>(T[] array)
    {
        int random1, random2;
        T temp;

        for (int i = 0; i < array.Length; ++i)
        {
            random1 = Random.Range(0, array.Length);
            random2 = Random.Range(0, array.Length);

            temp = array[random1];
            array[random1] = array[random2];
            array[random2] = temp;
        }

        return array;
    }
}
