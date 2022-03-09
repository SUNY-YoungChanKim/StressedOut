using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyPadFollow : MonoBehaviour
{
    [SerializeField]private float RaycastDistance=1.0f;
    private static JoyPadFollow instance;
    private Vector3 Rotateoffset;
    private RaycastHit hit;
    private bool Movable=true;
    // Start is called before the first frame update
    public static JoyPadFollow Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    private void Awake() 
    {
        instance=this;
        Rotateoffset=  this.transform.rotation.eulerAngles;
    }
    private void Update() {
        CheckMovable();
    }
    private void FixedUpdate() 
    {
        if(VirtualJoypad.Instance.getMoveStatus())
        {
            move(VirtualJoypad.Instance.getCacVec());
            Rotate(VirtualJoypad.Instance.getCacVec());
        }
    }

    // Update is called once per frame

    public void move(Vector2 direct)
    {
        if(Movable==true)
        {
            this.transform.position= Vector3.MoveTowards(this.transform.position,
            new Vector3(direct.x,0,direct.y)+this.transform.position,
            CharacterInfo.Instance.GetSpeed()*VirtualJoypad.Instance.getMoveMag()/2);
        }
    }
    private void CheckMovable()
    {
        if(Physics.Raycast(transform.position, -transform.right, out hit,RaycastDistance))
        {
            if(hit.transform.gameObject.tag=="Obstacles")
            {
                Movable=false;
                return;
            }
        }
        Movable=true;
    }
    public void Rotate(Vector2 drirect)
    {
        Quaternion Target=Quaternion.Euler(Rotateoffset.x,Rotateoffset.y,-(Mathf.Atan2(0+drirect.y,0+drirect.x)*180)/Mathf.PI+90.0f);
        
        this.transform.rotation= Quaternion.Slerp(this.transform.rotation,Target,0.1f);
    }
}
