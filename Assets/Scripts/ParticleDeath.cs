using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleDeath : MonoBehaviour
{
    public ParticleSystem partSys;
    float timeSinceMade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceMade += Time.deltaTime;
        if(timeSinceMade > partSys.main.duration) {
            Destroy(gameObject);
        }
    }
}
