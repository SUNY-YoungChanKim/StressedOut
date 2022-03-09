using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePreFabToCharacter : MonoBehaviour
{
    [SerializeField]private GameObject Object;
    // Start is called before the first frame update

    public void Call()
    {
        Instantiate(Object,CharacterInfo.Instance.transform);
    }
}
