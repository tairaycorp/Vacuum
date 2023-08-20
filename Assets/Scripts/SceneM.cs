using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneM : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) {
            GameManager.Difficulty = 0;
            RegenerateScene();
        }
    }

    public static void RegenerateScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}


