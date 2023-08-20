using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LookAtEnemyMoveType", menuName = "EnemyMoveType/LookAtEnemyMoveType", order = 1)]
public class LookAtEnemyMoveType : EnemyMoveType
{
    public override void Calculate(GameObject e, Transform t, Transform p, Transform startingT, float timeOfCreation) {
        Vector3 dir = p.position - t.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        //angle = angle * (Time.deltaTime + rotateSpeed);
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        
        t.rotation = Quaternion.Lerp(t.rotation, q, rotateSpeed * Time.deltaTime);
        Debug.DrawRay(t.GetChild(0).position, (t.forward) * 1000, Color.red);
        Physics2D.queriesHitTriggers = false;
        LayerMask mask = LayerMask.GetMask("Projectiles") | LayerMask.GetMask("Enemy");
        RaycastHit2D hit = Physics2D.Raycast(t.GetChild(0).position, t.up, Mathf.Infinity, ~mask);
        if (hit != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            
            if(hit.collider.tag == "Player") {
                e.GetComponent<ActiveEnemy>().Fire();
            }
            Debug.DrawRay(t.GetChild(0).position, t.GetChild(0).up * hit.distance, Color.red, 0.01f);
            //Debug.Log("Ray" + hit.collider.gameObject.name);
            
            
        }
        //t.rotation = Quaternion.Euler(0f, 0f, rot_Z - 90);
        //Vector2 direction = new Vector2(p.position.x - t.transform.position.x, p.position.y - t.transform.position.y);
        //t.transform.up = direction;
        //Debug.Log(q);
        //e.GetComponent<ActiveEnemy>().Fire();
    }
}
