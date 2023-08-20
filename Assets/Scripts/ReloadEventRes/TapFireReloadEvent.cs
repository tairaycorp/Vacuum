using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TapFireReloadEvent", menuName = "ReloadEventResponse/TapFireReloadEvent", order = 1)]
public class TapFireReloadEvent : ReloadEventResponse
{
    public override float Respond(Gun owner, Collision2D c = null, float d = 0f)
    {
        if(owner.newClick == true && ((Time.time - owner.timeStampFire) > fireWait)) {
            
            return 1f;
        } else {
            return 0f;
        }
    }
}
