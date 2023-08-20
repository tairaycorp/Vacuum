using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEnemy : MonoBehaviour
{

    public Enemy enemy;
    EnemyMoveType moveType;
    public GameObject player;
    float spawnTime;
    Transform startingTransform;
    public Transform spawnPoint;
    GameObject dmgNumbers;
    Transform damageTransform;
    public Transform gunTransform; 
    public bool alert = false;
    public float movementTimeStamp;
    public float movementStages;
    public GameObject deathParticles;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        enemy.Initialize();
        spawnTime = Time.time;
        moveType = enemy.enemyDef.moveType; 
        startingTransform = gameObject.transform;
        enemy.me = gameObject;

        enemy.weapon.Initialize(enemy.weapon.gunDef, gameObject);
        dmgNumbers = Resources.Load<GameObject>("UI/DamageNumbers/FloatingParent") as GameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
       moveType.Calculate(gameObject, gameObject.transform, player.transform, startingTransform, spawnTime); 
       if(gunTransform != null) {
           //gunTransform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
           gunTransform.gameObject.GetComponent<SpriteRenderer>().sprite = enemy.weapon.gunDef.sprite;
       }
       
    }

    public void Die() {
        if(deathParticles != null) {
            GameObject part = Instantiate(deathParticles, transform.position, transform.rotation);
            part.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 180);
        }

        Destroy(gameObject);
    }

    public void Fire() {
        if(enemy.weapon.OnReloadEvent(true)) {
            enemy.weapon.OnFireEvent();
            //newClick = false;
        }
    }

    public void DamageEffect(float damage, bool crit) {

        //damageTransform.position = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), transform.position.z);
        GameObject instanceDmg = Instantiate(dmgNumbers, new Vector3(transform.position.x + Random.Range(-2f, 2f), transform.position.y + Random.Range(0, 1f), transform.position.z), Quaternion.identity);
        instanceDmg.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        if(crit != true) {
            instanceDmg.transform.GetChild(0).GetComponent<TextMesh>().color = new Color(239, 220, 134);
        }
        
        //Debug.Log("Transform: " + transform.position);
        instanceDmg.transform.GetChild(0).localPosition = new Vector3(0, 0, 0);
    }
}
