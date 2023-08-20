using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FollowMouseProjectileMovementType", menuName = "ProjectileMovementType/FollowMouseProjectileMovementType", order = 1)]
public class FollowMouseProjectileMovementType : RandomSpreadMovementType
{
    bool hasTouchedMouse = true;
    public override void Calculate(GameObject p, Transform b, Transform startingT, float timeOfCreation) {

        // Vector3 mousePosition = Input.mousePosition;
        // mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // b.transform.position = Vector2.Lerp(b.transform.position, mousePosition, forwardSpeed);
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Debug.Log(hasTouchedMouse);


        //if(Math.Abs(mousePosition.x - b.transform.position.x) < 1 && Math.Abs(mousePosition.y - b.transform.position.y) < 1) {
            // hasTouchedMouse = false;
        //    Debug.Log("LOL");
        //} 

        if(p.GetComponent<Projectile>().hasTouchedMouse != true) {
            Debug.Log("HasntTouched");
            Vector2 direction = new Vector2(mousePosition.x - b.transform.position.x, mousePosition.y - b.transform.position.y);
            b.transform.up = direction;
        }
        
        b.transform.position += (b.transform.up * forwardSpeed * Time.deltaTime);

    }
}
