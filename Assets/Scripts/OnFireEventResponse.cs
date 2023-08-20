using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FireEventResponse : EventResponse
{

    public float dmg;


    public override float Respond(Gun owner, Collision2D c = null, float d = 0f)
    {
        priority = 10;

        return 5f;
    }

    public void doStuff() {

    }

    public void doStuff(int i) {

    }

    public void doStuff(string s) {
        
    }
}
