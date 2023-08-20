using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyTypeSpecficDamageCalcEventResponse", menuName = "DamageCalcEventResponse/EnemyTypeSpecficDamageCalcEventResponse", order = 1)]
class EnemyTypeSpecficDamageCalcEventResponse : DamageCalcEventResponse {
    Enemy target;
    public Enemy_Def.EnemyType enemyType;
    public override float Respond(Gun owner, Collision2D c, float d)
    {/*
        if(target != null && c.gameObject.GetComponent<Enemy_Def>() != null) {
            if(modifierType == DamageModifierType.Additive) {
                return d + modifierAmount;
            } else {
                return d * modifierAmount;
            }
            
        } else {
            return d;
        }*/
        return d;
    }
}
