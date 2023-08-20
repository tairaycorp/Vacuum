using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RandomSpreadMovementType", menuName = "ProjectileMovementType/RandomSpreadMovementType", order = 1)]
public class RandomSpreadMovementType : ProjectileMovementType
{
    public float forwardSpeed;
    public float angle = 10f;
    //Random rnd = new Random();

    public override void Calculate(GameObject p, Transform b, Transform startingT, float timeOfCreation) {
        if(p.GetComponent<Projectile>().firstFire == true) {
            b.transform.eulerAngles = new Vector3(b.transform.eulerAngles.x, b.transform.eulerAngles.y, p.GetComponent<Projectile>().gunDad.owner.transform.eulerAngles.z + Random.Range(-angle, angle));
        }
        
        b.transform.position += (b.transform.up * forwardSpeed * Time.deltaTime);
        // Debug.Log("Rotation:" + gameObject.transform.rotation.z);
    }
}
