using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OrbitSineProjectileMovementType", menuName = "ProjectileMovementType/OrbitSineProjectileMovementType", order = 1)]
public class OrbitSineProjectileMovementType : SineProjectileMovementType
{
    public override void Calculate(GameObject p, Transform b, Transform startingT, float timeOfCreation) {

        b.transform.position += (b.transform.right * Mathf.Cos((Time.time - timeOfCreation) * frequency) * amp) * Time.deltaTime;
        b.transform.position += (b.transform.up * Mathf.Sin((Time.time - timeOfCreation) * frequency) * amp) * Time.deltaTime;
        //b.transform.position = new Vector3(b.transform.position.x, b.transform.position.y - (amp / 2), b.transform.position.z);
        //b.transform.position = new Vector3(
        //b.transform.position.x + (b.transform.position.x * Mathf.Cos((Time.time - timeOfCreation) * frequency) * amp),
        //b.transform.position.y + (b.transform.position.y * Mathf.Cos((Time.time - timeOfCreation) * frequency) * amp), 
        //b.transform.position.z);
    }
}
