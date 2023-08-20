using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CheckLOSMoveType", menuName = "EnemyMoveType/CheckLOSMoveType", order = 1)]
public class CheckLOSMoveType : EnemyMoveType
{

    public override void Calculate(GameObject e, Transform t, Transform p, Transform startingT, float timeOfCreation) {
        ActiveEnemy act = e.GetComponent<ActiveEnemy>();
        if(Vector2.Distance(t.position, p.position) < visionDistance || act.alert == true) {
            act.alert = true;
            if(act.movementStages == 0) {
                LayerMask mask = LayerMask.GetMask("Projectiles") | LayerMask.GetMask("Enemy");
                RaycastHit2D hit = Physics2D.Raycast(t.position, t.up, Mathf.Infinity, ~mask);
                if (hit != null)
                {
                    if(hit.collider.tag == "Player") {
                        act.movementTimeStamp = Time.time;
                        act.movementStages = 3;
                    } else {
                        t.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
                        act.movementTimeStamp = Time.time;
                        act.movementStages = 2;
                    }
                }
                act.movementTimeStamp = Time.time;
                act.movementStages += 1;
            }
            else if(Time.time - act.movementTimeStamp < 0.8f && act.movementStages == 2) {
                Debug.Log("Moving");
                t.position += t.up * moveSpeed * Time.deltaTime;
            } else if(Time.time - act.movementTimeStamp < 1.5f && act.movementStages == 3) {
                Debug.Log("Rotating");
                Vector3 dir = p.position - t.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
                //angle = angle * (Time.time + rotateSpeed);
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                t.rotation = Quaternion.Lerp(t.rotation, q, rotateSpeed * Time.time);

                act.Fire();
            } else if(act.movementStages == 2 || act.movementStages == 3) {
                act.movementStages = 5;
            }
            else if(act.movementStages == 5) {
                //Debug.Log("Firing");
                act.movementTimeStamp = Time.time;
                act.Fire();
                act.movementStages += 1;
            }
            
            else if(act.movementStages == 6 && Time.time - act.movementTimeStamp < 1.5f) {
                //Debug.Log("Waiting");
                
            } else {
                act.movementStages = 0;
            }
        } else {
            
        }
        if(Vector2.Distance(t.position, p.position) < visionDistance * 2) {
            e.GetComponent<ActiveEnemy>().alert = false;
        }
    }
}
