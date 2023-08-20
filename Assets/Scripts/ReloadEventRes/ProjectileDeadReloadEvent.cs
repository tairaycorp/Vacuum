using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ProjectileDeadReloadEvent", menuName = "ReloadEventResponse/ProjectileDeadReloadEvent", order = 1)]
public class ProjectileDeadReloadEvent : ReloadEventResponse
{
    public override float Respond(Gun owner, Collision2D c = null, float d = 0f)
    {
        if(owner.projectiles.Count == 0 && (Time.time - owner.timeStampFire) > fireWait) {
            return 1f;
        } else {
            return 0f;
        }

        
    }
}
