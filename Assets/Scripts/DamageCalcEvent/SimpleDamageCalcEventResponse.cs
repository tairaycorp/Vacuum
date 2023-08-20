using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleDamageCalcEventResponse", menuName = "EventResponse/SimpleDamageCalcEventResponse", order = 1)]
class SimpleDamageCalcEventResponse : DamageCalcEventResponse {
    public override float Respond(Gun owner, Collision2D c, float d)
    {
        if(modifierType == DamageModifierType.Additive) {
            return d + modifierAmount;
        } else {
            return d * modifierAmount;
        }

    }
}
