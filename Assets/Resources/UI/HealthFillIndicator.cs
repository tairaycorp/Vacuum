using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthFillIndicator : MonoBehaviour
{
    public GameObject player;
    private PlaverMovement playerMove;
    Image me;
    float fill;
    // Start is called before the first frame update
    void Start()
    {
        playerMove = player.GetComponent<PlaverMovement>();
        me = GetComponent<Image>();
        fill = me.fillAmount;
    }

    // Update is called once per frame
    void Update()
    {
        me.fillAmount = (float)Mathf.Lerp (fill, playerMove.playerCharacter.health / playerMove.playerCharacter.charDef.health, Time.deltaTime / 0.2f);
        //me.fillAmount = playerMove.playerCharacter.health / playerMove.playerCharacter.charDef.health;
        fill = me.fillAmount;
    }
}
