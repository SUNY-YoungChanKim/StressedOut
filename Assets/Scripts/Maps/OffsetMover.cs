using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetMover : MonoBehaviour
{
    [SerializeField]private float Speed=1;
    List<Material> MList=new List<Material>();
    float offset=0;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<MeshRenderer>().GetMaterials(MList);

    }

    // Update is called once per frame
    void Update()
    {
        foreach(Material M in MList)
        {
            offset-=Time.timeScale*Speed;
            M.SetTextureOffset("_MainTex",new Vector2(0,offset));
        }
    }
}
