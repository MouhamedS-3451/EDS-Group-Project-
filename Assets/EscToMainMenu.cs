using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscToMainMenu : MonoBehaviour
{
    [HideInInspector]

    public LevelLoader LevelLoader;
    
    [HideInInspector]

    void Start() {
        LevelLoader = FindObjectOfType<LevelLoader>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            LevelLoader.LoadLevel("MainMenu");
        }
    }

    public void LoadLevel(string levelName) {
        LevelLoader.LoadLevel(levelName);
    }
}
