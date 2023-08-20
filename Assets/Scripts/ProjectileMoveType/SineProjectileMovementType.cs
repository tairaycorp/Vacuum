using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SineProjectileMovementType", menuName = "ProjectileMovementType/SineProjectileMovementType", order = 1)]
public class SineProjectileMovementType : ProjectileMovementType {
    public float frequency;
    public float amp;
    public float forwardSpeed;


    public override void Calculate(GameObject p, Transform b, Transform startingT, float timeOfCreation) {

        b.transform.position += b.transform.right * Mathf.Cos((Time.time - timeOfCreation) * frequency) * amp;
        b.transform.position += (b.transform.up * forwardSpeed * Time.deltaTime);
    }
}
