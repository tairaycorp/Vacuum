using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageCalcEventResponse : EventResponse {
    public enum DamageModifierType {Additive, Multiplicative}

    public DamageModifierType modifierType;
    public float modifierAmount;

    public float critChance;
    public float critMod;
}
