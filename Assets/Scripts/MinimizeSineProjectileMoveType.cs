using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MinimizeSineProjectileMovementType", menuName = "ProjectileMovementType/MinimizeSineProjectileMovementType", order = 1)]
public class MinimizeSineProjectileMoveType : SineProjectileMovementType {

    public float decrease = 30;
    public override void Calculate(GameObject p, Transform b, Transform startingT, float timeOfCreation) {
        float shortenAmp = ((Time.time - timeOfCreation) * 2) / (decrease * decrease);
        shortenAmp *= 2;
        if(shortenAmp < 1 ) {
            shortenAmp = 1;
        }
        if(Time.time - timeOfCreation < 1) {
            b.transform.position += b.transform.right * Mathf.Cos((Time.time - timeOfCreation) * frequency) * (amp / shortenAmp);
            b.transform.position += (b.transform.up * forwardSpeed * Time.deltaTime);
        } else {
            p.GetComponent<Rigidbody2D>().gravityScale = 3;
        }
        
    }
}
