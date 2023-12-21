using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDeath : Interactable
{
  [SerializeField] private GameObject player;
  public override void InRangeAction()
  {
    player.GetComponent<PlayerRespawn>().Respawn();
  }
}