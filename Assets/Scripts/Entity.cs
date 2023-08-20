using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity
{
    public float health;
    public float speed;
    public virtual void Damage(float damage, bool crit) {
        health = health - damage;
        //Debug.Log("Damagin");
        if(health <= 0) {
            Death();
        }
    }

    public virtual void Death() {}
}