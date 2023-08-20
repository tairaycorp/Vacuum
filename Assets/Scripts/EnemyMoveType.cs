using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveType : ScriptableObject
{
    public float moveSpeed;
    public float rotateSpeed;
    public float visionDistance;
    public float visionDegree = 30f;
    public virtual void Calculate(GameObject e, Transform t, Transform p, Transform startingT, float timeOfCreation) {
        
    }
}
