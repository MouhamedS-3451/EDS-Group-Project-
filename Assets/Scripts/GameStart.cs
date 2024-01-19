using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
  GameManager gameManager;

  void Start()
  {
    gameManager = FindObjectOfType<GameManager>();
    gameManager.LoadLevel("MainMenu");
  }
}
