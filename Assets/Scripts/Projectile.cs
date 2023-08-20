using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Transform startingTransform;
    public Gun gunDad;
    public Projectile_Def projectile_Def;
    public List<Attachment> attachments = new List<Attachment>();
    public SimpleProjectileOnFireEventResponse ogEventResponse;
    public float spawnTime;
    ProjectileMovementType projectileMovementType;
    public bool hasTouchedMouse = false;
    public bool firstFire = true;
    public GameObject particleSystem;


    void Start() {
        spawnTime = Time.time;
        attachments = gunDad.a;
    }

    void FixedUpdate() {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //Debug.Log(hasTouchedMouse);

        if(Math.Abs(mousePosition.x - transform.position.x) < 0.5 && Math.Abs(mousePosition.y - transform.position.y) < 0.5) {
            hasTouchedMouse = true;
        } 
        projectileMovementType.Calculate(gameObject, transform, startingTransform, spawnTime);
        firstFire = false;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject part;
        if(col.gameObject.tag != "Projectile" && col.gameObject != gunDad.owner) {
            gunDad.OnHitEvent(col);
            if(col.gameObject.tag == "Environment") {
                part = Instantiate(particleSystem, col.transform.position, col.transform.rotation);
                part.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 180);
            }
            
            
            // ALL THE OTHER COOL STUFF
            DestroySelf();
        }


    }

    public void Initialize(Gun g, Projectile_Def pd, ProjectileMovementType pmt) {
        //Debug.Log("Rotation:" + gameObject.transform.rotation.z);
        attachments = g.a;
        gunDad = g;
        gunDad.projectiles.Add(gameObject);
        projectile_Def = pd;
        projectileMovementType = pmt;
        gameObject.GetComponent<SpriteRenderer>().sprite = projectile_Def.sprite;

    }

    public void DestroySelf() {
        gunDad.projectiles.Remove(gameObject);
        Destroy(gameObject);

    }
}