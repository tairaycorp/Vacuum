using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class EventResponse : ScriptableObject {
    public float duration = 0f;
    List<EventResponseCondition> conditions = new List<EventResponseCondition>();
    public int priority = 0;

    public abstract float Respond(Gun owner, Collision2D c, float d);
}

public class PlayerHitEventResponse : EventResponse {
    public override float Respond(Gun owner, Collision2D c, float d) {
        Debug.Log("Beginning of PlayerHit");
        return 5.0005f;
    }
}
public class FatalHitEventResponse : EventResponse {
    public override float Respond(Gun owner, Collision2D c, float d) {
        Debug.Log("Beginning of FatalHit");
        return 5.000005f;
    }
}

