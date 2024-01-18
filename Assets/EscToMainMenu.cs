using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscToMainMenu : MonoBehaviour
{
    [HideInInspector]

    public LevelLoader levelLoader;
    
    [HideInInspector]

    void Start() {
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            LevelLoader.LoadLevel("MainMenu");
        }
    }
}
