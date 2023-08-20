using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BasicReloadEventResponse", menuName = "ReloadEventResponse/BasicReloadEventResponse", order = 1)]
public class BasicReloadEventResponse: ReloadEventResponse
{
    public override float Respond(Gun owner, Collision2D c = null, float d = 0f)
    {
        if((Time.time - owner.timeStampFire) > fireWait) {
            return 1f;
        } else {
            return 0f;
        }

        
    }
}
