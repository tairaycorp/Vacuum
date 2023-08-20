using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RecoilEventResponse : EventResponse
{
    public float velocity;
    public override float Respond(Gun owner, Collision2D c = null, float d = 0f)
    {
        

        return velocity;
    }
}
