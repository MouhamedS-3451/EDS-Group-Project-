using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDeath : Interactable
{
  [SerializeField] private GameObject player;
  public bool isWater = false;
  private AudioManager audioManager;

  void Awake()
  {
    audioManager = FindObjectOfType<AudioManager>();
    if (gameObject.GetComponent<SpriteRenderer>() != null)
    {
      gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
  }

  public override void InRangeAction()
  {
    player.GetComponent<PlayerRespawn>().Respawn();
    if (isWater) audioManager.Play("Dive");
  }
}