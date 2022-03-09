using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceMover : MonoBehaviour
{
    // Start is called before the first frame update
    public void SequenceMove()
    {
        CardSystem.Instance.SequenceMove();
        CardSystem.Instance.RemoveCard(transform.parent.gameObject);
    }
}
