using UnityEngine;

public class MenuButtons : MonoBehaviour
{

  LevelLoader levelLoader;
  public GameObject resetConfirmation1;
  public GameObject resetConfirmation2;
  public GameObject quitConfirmation;

  void Start()
  {
    levelLoader = FindObjectOfType<LevelLoader>();
    transform.GetChild(2).gameObject.SetActive(false);
    resetConfirmation1.SetActive(false);
    resetConfirmation2.SetActive(false);
    quitConfirmation.SetActive(false);
    AudioManager audioManager = FindObjectOfType<AudioManager>();

    audioManager.Play("ThemeMainMenu");
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      CloseSettings();
    }
  }

  public void GoToScene(string sceneName)
  {
    levelLoader.LoadLevel(sceneName);
  }

  public void ShowQuitConfirmation()
  {
    quitConfirmation.SetActive(true);
  }

  public void Quit()
  {
    Application.Quit();
  }

  public void Play()
  {
    int unlockedLevel = FindObjectOfType<GameManager>().unlockedLevel;
    switch (unlockedLevel)
    {
      case 1:
        GoToScene("Level 1");
        break;
      case 2:
        GoToScene("Level 2");
        break;
      case 3:
        GoToScene("Level 3");
        break;
      default:
        GoToScene("LevelSelect");
        break;
    }
  }

  public void OpenSettings()
  {
    transform.GetChild(2).gameObject.SetActive(true);
  }

  public void CloseSettings()
  {
    transform.GetChild(2).gameObject.SetActive(false);
  }

  public void ShowResetConfirmation1()
  {
    resetConfirmation1.SetActive(true);
  }

  public void ShowResetConfirmation2()
  {
    resetConfirmation1.SetActive(false);
    resetConfirmation2.SetActive(true);
    
  }

  public void ResetProgress()
  {
    GameManager gameManager = FindObjectOfType<GameManager>();
    gameManager.ResetProgress();
    levelLoader.LoadLevel("MainMenu");
  }
}
