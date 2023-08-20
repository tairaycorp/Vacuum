using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicRecoilEventResponse", menuName = "RecoilEventResponse/BasicRecoilEventResponse", order = 1)]
public class BasicRecoilEvent : RecoilEventResponse
{
    public override float Respond(Gun owner, Collision2D c = null, float d = 0f)
    {
        

        return velocity;
    }
}
