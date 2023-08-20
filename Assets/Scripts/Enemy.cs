using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy : Entity 
{
    public Gun weapon;
    public Enemy_Def enemyDef;
    public GameObject me;

    public void Initialize() {
        health = enemyDef.health;
        weapon.gunDef = enemyDef.weapon;

    }

    public override void Death() {

        me.GetComponent<ActiveEnemy>().Die();
    }

    public override void Damage(float damage, bool crit) {
        health = health - damage;
        Debug.Log("Damagin");
        if(health <= 0) {
            Death();
        }
        me.GetComponent<ActiveEnemy>().DamageEffect(damage, crit);
    }

    

}
