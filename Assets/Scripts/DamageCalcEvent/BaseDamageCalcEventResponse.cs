using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseDamageCalcEventResponse", menuName = "DamageCalcEventResponse/BaseDamageCalcEventResponse", order = 1)]
public class BaseDamageCalcEventResponse : DamageCalcEventResponse
{

    public override float Respond(Gun owner, Collision2D c, float d)
    {
        if(modifierType == DamageModifierType.Additive) {
                return d + modifierAmount;
            } else {
                return d * modifierAmount;
            }
    }
}
