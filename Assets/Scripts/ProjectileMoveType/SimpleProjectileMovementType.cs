using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleProjectileMovementType", menuName = "ProjectileMovementType/SimpleProjectileMovementType", order = 1)]
public class SimpleProjectileMovementType : ProjectileMovementType {
    public float forwardSpeed;

    public override void Calculate(GameObject p, Transform b, Transform startingT, float timeOfCreation) {

        b.transform.position += (b.transform.up * forwardSpeed * Time.deltaTime);
    }
}