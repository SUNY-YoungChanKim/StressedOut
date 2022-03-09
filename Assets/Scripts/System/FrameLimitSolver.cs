using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameLimitSolver : MonoBehaviour
{
    private static FrameLimitSolver instance;
    public static FrameLimitSolver Instance
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
    // Start is called before the first frame update
    private void Awake() 
    {
        instance=this;
        Changeto60FPS();
    }
    public void Changeto30FPS()
    {
        Application.targetFrameRate=30;
    }
    public void Changeto60FPS()
    {
        Application.targetFrameRate=60;
    }
}
