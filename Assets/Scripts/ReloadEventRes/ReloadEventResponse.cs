using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadEventResponse : EventResponse
{
    public float fireWait;
    public override float Respond(Gun owner, Collision2D c = null, float d = 0f)
    {
        

        return 0f;
    }
}
