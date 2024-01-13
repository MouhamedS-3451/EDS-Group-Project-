using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

  public GameObject pauseMenu;

  [HideInInspector]
  public bool isPaused = false;

  // Start is called before the first frame update
  void Awake()
  {
    pauseMenu.SetActive(false);
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
    pauseMenu.SetActive(false);
    Time.timeScale = 1f;
    isPaused = false;
  }

  void Pause()
  {
    pauseMenu.SetActive(true);
    Time.timeScale = 0f;
    isPaused = true;
  }

  public void LoadMenu()
  {
    Time.timeScale = 1f;
    FindObjectOfType<GameManager>().LoadMenu();
  }
}
