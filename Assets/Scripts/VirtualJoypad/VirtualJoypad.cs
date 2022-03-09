using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class VirtualJoypad : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]private float MaxDis,Disratio;
    private static VirtualJoypad instance;
    Vector3 start,current;
    private bool move;
    private Vector2 CacVec;
    private float MoveMag;
    public static VirtualJoypad Instance
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
        move=false;
    }
    private void Update() {
        CacVec=getDircetion();
        MoveMag=Mathf.Abs(CacVec.x)>Mathf.Abs(CacVec.y)? Mathf.Abs(CacVec.x):Mathf.Abs(CacVec.y);
        if(move==true) 
        {
         
            VirtualTrailImage.Instance.ChangeColor(MoveMag);
            VirtualTrailImage.Instance.ChangeSize(MoveMag);
            VirtualPadManage.Instance.ChangeSize(MoveMag);
        }
    }
    // Start is called before the first frame update
    public void OnBeginDrag(PointerEventData eventData) 
    {

        start=eventData.position;
        VirtualPadManage.Instance.Popup(start);
        VirtualTrail.Instance.InitInstance(start);
    } 
    public void OnDrag(PointerEventData eventData) 
    {
        move=true;
        current=eventData.position;
        VirtualTrail.Instance.UpdateInstance(current);
    } 
    public void OnEndDrag(PointerEventData eventData)
    { 
         move=false;
         VirtualPadManage.Instance.PopDown();
         VirtualTrail.Instance.UnableInstance();
         start=current=new Vector3(0,0,0);
    }
    public Vector2 getCacVec()
    {
        return CacVec;
    }
    public float getMoveMag()
    {
        return MoveMag;
    }
    public bool getMoveStatus()
    {
        return move;
    }
    public Vector2 getDircetion()
    {
        float Xdis,Ydis;
        Vector2 result;
        Xdis=start.x>current.x ? (start.x-current.x) : current.x-start.x; 
        Ydis=start.y>current.y ? (start.y-current.y) : current.y-start.y;
        Xdis=Xdis>MaxDis? MaxDis:Xdis;
        Ydis=Ydis>MaxDis? MaxDis:Ydis;
        Xdis=start.x>current.x ? Xdis*-1 : Xdis; 
        Ydis=start.y>current.y ? Ydis*-1 : Ydis;



        result=new Vector2(Xdis/Disratio,Ydis/Disratio);

        return result;
    }

     private void OnDisable() 
    {
        OnEndDrag(null);     
    }
}
