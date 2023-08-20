using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun_Def", menuName = "Gun_Def", order = 1)]
public class Gun_Def : ScriptableObject
{
    public List<Attachment> baseAttachments = new List<Attachment>();
    public Sprite sprite;
    public GunPos gunPos;
    public int magSize;
    
    
}
public enum GunPos {rightHandSmall, leftHandSmall, centerBig}