using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FollowPlayerEnemyMoveType", menuName = "EnemyMoveType/FollowPlayerEnemyMoveType", order = 1)]
public class FollowPlayerEnemyMoveType : EnemyMoveType
{

    public override void Calculate(GameObject e, Transform t, Transform p, Transform startingT, float timeOfCreation) {
        if(Vector2.Distance(t.position, p.position) < visionDistance || e.GetComponent<ActiveEnemy>().alert == true) {
            e.GetComponent<ActiveEnemy>().Fire();
            e.GetComponent<ActiveEnemy>().alert = true;
            t.position += t.up * moveSpeed * Time.deltaTime;
            Vector3 dir = p.position - t.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            //angle = angle * (Time.deltaTime + rotateSpeed);
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            t.rotation = Quaternion.Lerp(t.rotation, q, rotateSpeed * Time.deltaTime);
            // Debug.DrawRay(t.GetChild(0).position, (t.forward) * 1000, Color.red);

            LayerMask mask = LayerMask.GetMask("Projectiles") | LayerMask.GetMask("Enemy");
            RaycastHit2D hit = Physics2D.Raycast(t.position, t.up, Mathf.Infinity, ~mask);
            if (hit != null)
            {
                if(hit.collider.tag == "Player") {
                    e.GetComponent<ActiveEnemy>().Fire();
                }
                Debug.DrawRay(t.position, t.up * hit.distance, Color.red, 0.01f);
                //Debug.Log("Ray" + hit.collider.gameObject.name);
                
                
            }
        } else {
            //Debug.Log("Too far");
        }
        if(Vector2.Distance(t.position, p.position) < visionDistance * 2) {
            e.GetComponent<ActiveEnemy>().alert = false;
        }
    }
}
