using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleProjectileOnFireEventResponse", menuName = "EventResponse/SimpleProjectileOnFireEventResponse", order = 1)]
public class SimpleProjectileOnFireEventResponse : FireEventResponse
{
    public float targetVelocity;
    public float deathTimer;

    public Projectile_Def myProjectile;
    public GameObject prefab;

    public ProjectileMovementType moveType;
    public int amtOfProj;

    public override float Respond(Gun owner, Collision2D c, float d)
    {
        int i = 0;
        while(amtOfProj > i) {
            //Debug.Log("SpawnPoint" + owner.spawnPoint.rotation.z);
            Instantiate(prefab, owner.spawnPoint.position, owner.spawnPoint.rotation).GetComponent<Projectile>().Initialize(owner, myProjectile, moveType);
            i ++;
        }    
        
        return 0f;
    }

    public void Movement(GameObject garry) {
        garry.transform.Translate(Vector3.up * targetVelocity * Time.deltaTime);
    }
}
