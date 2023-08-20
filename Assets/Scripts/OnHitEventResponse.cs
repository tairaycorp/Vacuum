using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitEventResponse : EventResponse {
    public override float Respond(Gun owner, Collision2D c, float d) {
        return 5.0005f;
    }
}
