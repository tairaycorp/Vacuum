using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character : Entity 
{
    public Gun weapon;
    public Character_Def charDef;
    public GameObject me;

    public void Initialize() {
        health = charDef.health;
        weapon.gunDef = charDef.weapon;
        speed = charDef.speed; 

    }

    public override void Death() {
        me.GetComponent<PlaverMovement>().Die();
    }
}