using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LookAtEnemyIfEnemyVisibleMoveType", menuName = "EnemyMoveType/LookAtEnemyIfEnemyVisibleMoveType", order = 1)]
public class LookAtEnemyIfEnemyVisible : EnemyMoveType
{
    public override void Calculate(GameObject e, Transform t, Transform p, Transform startingT, float timeOfCreation) {
        LayerMask mask = LayerMask.GetMask("Projectiles") | LayerMask.GetMask("Enemy");
        
        Vector3 dir = p.position - t.position;
        RaycastHit2D hit = Physics2D.Raycast(t.GetChild(0).position, t.up, Mathf.Infinity, ~mask);
        RaycastHit2D playerHit = Physics2D.Raycast(t.GetChild(0).position, dir, Mathf.Infinity, ~mask);
        Debug.DrawRay(t.position, dir * playerHit.distance, Color.red, 0.01f);
        Debug.Log("Ray" + playerHit.collider.gameObject.name);
        if(playerHit.collider.tag == "Player") {
            e.GetComponent<ActiveEnemy>().Fire();
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            //angle = angle * (Time.deltaTime + rotateSpeed);
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            t.rotation = Quaternion.Lerp(t.rotation, q, rotateSpeed * Time.deltaTime);
            // Debug.DrawRay(t.GetChild(0).position, (t.forward) * 1000, Color.red);

            
            if (hit != null)
            {
                if(hit.collider.tag == "Player") {
                    
                }
                
                //Debug.Log("Ray" + hit.collider.gameObject.name);
                   
            }
        }
        

        //Debug.DrawRay(t.GetChild(0).position, t.GetChild(0).up * hit.distance, Color.red, 0.01f);
    }
}
