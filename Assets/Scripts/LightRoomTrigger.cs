using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LightRoomTrigger : MonoBehaviour
{

    public UnityEngine.Rendering.Universal.Light2D light;
    public Collider2D trigger;
    public List<Vector3Int> blockers;
    float lightIntensity;
    bool increasing = false;
    public GameObject enemyParent;
    public Tilemap walls;
    bool tried = false;
    // Start is called before the first frame update
    void Start()
    {
        lightIntensity = light.intensity;
        light.intensity = 0;
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        if(increasing == true) {
            light.intensity = (float)Mathf.Lerp (light.intensity, lightIntensity, Time.deltaTime / 0.2f);
        } else {
            light.intensity = (float)Mathf.Lerp (light.intensity, 0, Time.deltaTime / 0.2f);
        }
        
        if(enemyParent.transform.childCount <= 0 && tried == false) {
            foreach(Vector3Int g in blockers) {
                walls.SetTile(g, null); // Remove tile at 0,0,0
            }
            tried = true;
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.tag == "Player") {
            increasing = true;
            foreach (Transform child in transform) {
                child.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if(col.tag == "Player") {
            increasing = false;
        }
    }
}
