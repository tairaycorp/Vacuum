using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public Sprite sprite;
    public Sprite secondSprite;
    public PlaverMovement playerMove;
    bool opened = false;
    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<SpriteRenderer>().sprite = sprite;
    }

    public void Interaction() {
        if(!opened) {
            Debug.Log("Interacted");
            GetComponent<SpriteRenderer>().sprite = secondSprite;
            Gun_Def newGun = GameManager.NewPlayerWeapon();
            playerMove.NewGun(newGun);
            opened = true;
        }
        
    }
}
