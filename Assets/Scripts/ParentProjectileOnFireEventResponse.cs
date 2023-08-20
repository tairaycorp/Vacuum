using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParentProjectileOnFireEventResponse", menuName = "EventResponse/ParentProjectileOnFireEventResponse", order = 1)]
public class ParentProjectileOnFireEventResponse : SimpleProjectileOnFireEventResponse
{
    public Projectile_Def myProjectileChild;

    public ProjectileMovementType moveTypeChild;

    public override float Respond(Gun owner, Collision2D c, float d)
    {
        Debug.Log("Parent Respond Start");
        GameObject parentClone = Instantiate(prefab);
        parentClone.GetComponent<Projectile>().Initialize(owner, myProjectile, moveType);
        GameObject childClone = Instantiate(prefab);
        childClone.GetComponent<Projectile>().Initialize(owner, myProjectileChild, moveTypeChild);
        childClone.transform.parent = parentClone.transform;
        return 0f;
    }
}
