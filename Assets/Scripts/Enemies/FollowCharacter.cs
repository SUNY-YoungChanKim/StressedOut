using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowCharacter : MonoBehaviour
{
    private NavMeshAgent AI;
    // Start is called before the first frame update
    void Start()
    {
        AI=this.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(AI.isOnNavMesh)AI.SetDestination(CharacterInfo.Instance.transform.position);
    }
}
