using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagEmptyEventResponse", menuName = "ReloadEventResponse/MagEmptyEventResponse", order = 1)]
public class MagEmptyEventResponse : EventResponse

{
    public float fireWait;
    public override float Respond(Gun owner, Collision2D c = null, float d = 0f)
    {
        // if(owner.magSize < 1 && ((Time.time - owner.timeStampFire) > fireWait)) {
        //     Debug.Log("Mag Empty Can Fire");
        //     return 0f;
        // }

        // return 1f;

        if(owner.magSize < 1) {
            if((Time.time - owner.timeStampFire) > fireWait) {
                owner.magSize = owner.gunDef.magSize;
                return 0f;
            } else {
                return 1f;
            }
            
        } else {
            return 0f;
        }
    }

}
