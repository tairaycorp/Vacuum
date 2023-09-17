using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// test
[CreateAssetMenu(fileName = "Attachment", menuName = "Attachment", order = 1)]
public class Attachment : ScriptableObject
{
    public List<EventResponse> e = new List<EventResponse>();
}