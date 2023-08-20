using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaverMovement : MonoBehaviour
{      
    public Gun_Def startWeapon;
    public float playerSpeed = 1000f;
    public Gun playerWeapon;
    float timeReset = 0.1f;
    public bool newClick = false;
    public Transform gunTransform;
    public Character playerCharacter;
    Rigidbody2D rb;
    public bool knockBack;
    public float knockbackSmooth;
    public float timer;
    public List<GameObject> interactables;
    public TileAutomata tileA;
    public TMP_Text magText;
    public TMP_Text healthText;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerWeapon.Initialize(GameManager.NewPlayerWeapon(), gameObject);
        playerCharacter.Initialize();
        if(playerWeapon.gunDef.gunPos == GunPos.leftHandSmall) {
            gunTransform.position = new Vector3(transform.position.x - 0.76f, transform.position.y + 0.75f, transform.position.z);
        } else if(playerWeapon.gunDef.gunPos == GunPos.rightHandSmall) {
            gunTransform.position = new Vector3(transform.position.x + 0.46f, transform.position.y + 0.8f, transform.position.z);
        } else {
            gunTransform.position = new Vector3(transform.position.x, transform.position.y + 1.64f, transform.position.z);
        }
        gunTransform.gameObject.GetComponent<SpriteRenderer>().sprite = playerWeapon.gunDef.sprite;
    }

    void Awake() {

    }
    void Update() {
        if(playerWeapon.gunDef != null) {
            if(playerWeapon.timeStampFire + playerWeapon.reloadTimeDuration < Time.time && playerWeapon.magSize < 1) {
                magText.text = playerWeapon.gunDef.magSize.ToString();
            } else {
                magText.text = playerWeapon.magSize.ToString();
            }
            healthText.text = playerCharacter.health + "/" + playerCharacter.charDef.health;
        } else {
            magText.text = "";
        }
        
        if(Input.GetMouseButtonDown(0)) {
            newClick = true;
        }
        // NOT WORKING BECAUSE WEAPON HASNT BEEN GENERATED. NEW INITIATIVE // SET VALUES MANUALLY.
        if(Input.GetMouseButton(0) && playerWeapon.OnReloadEvent(newClick)) {
            playerWeapon.OnFireEvent();
            newClick = false;
        }

        if(Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("Pressed E");
            interactables[0].GetComponent<Interactable>().Interaction();
        }

        if(Input.GetKeyDown(KeyCode.R)) {
          
        }
        
    }
    void FixedUpdate() {
        playerSpeed = SpeedCalc();
        if(knockBack == false) {
            rb.drag = 0;
            Vector2 targetVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            rb.velocity = targetVelocity * playerSpeed;
        } else {
            Debug.Log("KNOCK");
            if(timer < 0.1f) {
                rb.drag = 30;
                //transform.position = Vector3.SmoothDamp(transform.position,transform.up - 5, Vector3.zero, dampTime);
                
                timer += Time.deltaTime;
            //timer += Time.deltaTime;
            } else {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = 0f;
                rb.drag = 0;
                rb.velocity = Vector2.zero;
                knockBack = false;
            }
        }

    }

    public void Die() {
        gameObject.SetActive(false);
    }

    public void Knockback(float force) {
        knockBack = true;
        rb.AddForce((-transform.up) * force, ForceMode2D.Impulse);
        timer = 0;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Entered");
        if(col.tag == "Interactable") {
            interactables.Add(col.gameObject);
            Debug.Log(interactables.Count);

        if(col.tag == "Portal") {
            Debug.Log("NextLevel");
        }
        }
       
    }

    void OnTriggerExit2D(Collider2D col) {
        if(col.tag == "Interactable") {
            interactables.Remove(col.gameObject);
            Debug.Log("Exit");
        }
    }

    public void NewGun(Gun_Def gun) {
        playerWeapon.Initialize(gun, gameObject);
        if(playerWeapon.gunDef.gunPos == GunPos.leftHandSmall) {
            gunTransform.position = new Vector3(transform.position.x - 0.76f, transform.position.y + 0.75f, transform.position.z);
        } else if(playerWeapon.gunDef.gunPos == GunPos.rightHandSmall) {
            gunTransform.position = new Vector3(transform.position.x + 0.46f, transform.position.y + 0.8f, transform.position.z);
        } else {
            gunTransform.position = new Vector3(transform.position.x, transform.position.y + 1.64f, transform.position.z);
        }
        gunTransform.gameObject.GetComponent<SpriteRenderer>().sprite = playerWeapon.gunDef.sprite;
    }

    public float SpeedCalc() {
        float newSpeed = playerCharacter.speed;
        return newSpeed;

    }
}
