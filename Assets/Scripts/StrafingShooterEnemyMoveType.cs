using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StrafingShooterEnemy", menuName = "EnemyMoveType/StrafingShooterEnemy", order = 1)]
public class StrafingShooterEnemyMoveType : EnemyMoveType
{
    public override void Calculate(GameObject e, Transform t, Transform p, Transform startingT, float timeOfCreation) {
        ActiveEnemy act = e.GetComponent<ActiveEnemy>();
        if(Vector2.Distance(t.position, p.position) < visionDistance || act.alert == true) {
            act.alert = true;
            //act.movementTimeStamp = Time.time;
            //Debug.Log(act.movementTimeStamp);
            if(act.movementStages == 0) {
                t.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
                act.movementTimeStamp = Time.time;
                act.movementStages += 1;
            }
            else if(Time.time - act.movementTimeStamp < 1.3f && act.movementStages == 1) {
                //Debug.Log("Moving");
                t.position += t.up * moveSpeed * Time.deltaTime;
            }
            else if(act.movementStages == 1) {
                act.movementTimeStamp = Time.time;
                act.movementStages += 1;
            }
            else if(Time.time - act.movementTimeStamp < 0.8f && act.movementStages == 2) {
                //Debug.Log("Rotating");
                Vector3 dir = p.position - t.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
                //angle = angle * (Time.time + rotateSpeed);
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                t.rotation = Quaternion.Lerp(t.rotation, q, rotateSpeed * Time.time);
                //t.position += t.up * moveSpeed * Time.deltaTime;
                act.Fire();
                // Debug.DrawRay(t.GetChild(0).position, (t.forward) * 1000, Color.red);
            }
            else if(act.movementStages == 2) {
                //Debug.Log("Firing");
                act.movementTimeStamp = Time.time;
                t.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
                act.movementStages += 1;
            }
            
            else if(act.movementStages == 3 && Time.time - act.movementTimeStamp < 1.5f) {
                //Debug.Log("Waiting");
                t.position += t.up * moveSpeed * Time.deltaTime;
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
