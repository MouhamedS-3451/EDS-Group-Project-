using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
  // Start is called before the first frame update
  public Animator transition;

  public float transitionTime = 1f;

  // Update is called once per frame
  void Update()
  {

  }
  public void LoadNextLevel()
  {
    string levelName = SceneManager.GetActiveScene().name;
    string nextLevelName = "";

    nextLevelName = levelName switch
    {
      "Level 1" => "Level 2",
      "Level 2" => "Level 3",
      _ => "LevelSelect",
    };
    LoadLevel(nextLevelName);
  }

  public void LoadLevel(string levelName)
  {
    StartCoroutine(LoadLevelCoroutine(levelName));
  }

  IEnumerator LoadLevelCoroutine(string levelName)
  {
    //Play Animation
    transition.SetTrigger("Start");

    //Wait
    yield return new WaitForSeconds(transitionTime);
    //Load Scene
    GameManager gameManager = FindObjectOfType<GameManager>();
    gameManager.LoadLevel(levelName);
  }
}
