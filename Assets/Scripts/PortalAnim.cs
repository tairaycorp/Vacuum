using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAnim : MonoBehaviour
{
    public ParticleSystem aura;
    public ParticleSystem spawn;
    // Start is called before the first frame update
    void Start()
    {
        aura.Play();

        spawn.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 1, Space.Self);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Player") {
            GameManager.Difficulty += 1;
            SceneM.RegenerateScene();
        }
    }
}
