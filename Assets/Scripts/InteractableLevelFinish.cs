using UnityEngine;

public class LevelFinish : Interactable
{

  void Start()
  {
    //GetComponent<SpriteRenderer>().enabled = false;
  }
  
  public override void InRangeAction()
  {
    GameManager gameManager = FindAnyObjectByType<GameManager>();
    LevelLoader levelLoader = FindAnyObjectByType<LevelLoader>();
    GameObject player = FindAnyObjectByType<PlayerMovement>().gameObject;
    GameObject camera = FindAnyObjectByType<CameraFollow>().gameObject;

    gameManager.LevelComplete();
    player.GetComponent<PlayerMovement>().autoWalk = true;
    player.GetComponent<PlayerJumping>().active = false;
    camera.GetComponent<CameraFollow>().active = false;
    levelLoader.LoadNextLevel();


  }
}
