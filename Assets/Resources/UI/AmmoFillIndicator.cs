using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoFillIndicator : MonoBehaviour
{
    public GameObject player;
    private PlaverMovement playerMove;
    Image me;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = player.GetComponent<PlaverMovement>();
        me = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerMove.playerWeapon.magSize >= 1) {
            //Debug.Log("MagSize > 1");
            float tempMagSize = playerMove.playerWeapon.magSize;
            me.fillAmount = tempMagSize / playerMove.playerWeapon.gunDef.magSize;
            //Debug.Log(playerMove.playerWeapon.magSize / playerMove.playerWeapon.gunDef.magSize);
            //Debug.Log(playerMove.playerWeapon.gunDef.magSize);
        } else {
            //Debug.Log("MagSize < 1");
            me.fillAmount = (Time.time - playerMove.playerWeapon.timeStampFire) / playerMove.playerWeapon.reloadTimeDuration;
        }
    }
}
