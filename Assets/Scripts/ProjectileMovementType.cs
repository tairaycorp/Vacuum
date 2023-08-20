using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileMovementType : ScriptableObject
{
    public virtual void Calculate(GameObject p, Transform b, Transform startingT, float timeOfCreation) {
        
    }
}



