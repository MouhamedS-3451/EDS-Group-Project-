using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

  [HideInInspector]
  public GameManager gameManager;

  public LevelLoader levelLoader;

  public GameObject pauseMenuUI;

  [HideInInspector]
  public bool isPaused = false;

  // Start is called before the first frame update
  void Awake()
  {
    gameManager = FindObjectOfType<GameManager>();
    levelLoader = FindObjectOfType<LevelLoader>();
    pauseMenuUI.SetActive(false);
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (isPaused)
      {
        Resume();
      }
      else
      {
        Pause();
      }
    }
  }

  public void Resume()
  {
    pauseMenuUI.SetActive(false);
    Time.timeScale = 1f;
    isPaused = false;
  }

  private void Pause()
  {
    pauseMenuUI.SetActive(true);
    Time.timeScale = 0f;
    isPaused = true;
  }

  public void Restart()
  {
    Time.timeScale = 1f;
    levelLoader.LoadLevel(SceneManager.GetActiveScene().name);
  }

  public void LoadMenu()
  {
    Time.timeScale = 1f;
    levelLoader.LoadLevel("MainMenu");
  }

  public void LoadLevelSelect()
  {
    Time.timeScale = 1f;
    levelLoader.LoadLevel("LevelSelect");
  }
}
