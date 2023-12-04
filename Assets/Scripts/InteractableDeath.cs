using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDeath : Interactable
{
  public override void InRangeAction()
  {
    GameObject.Find("Player").GetComponent<PlayerRespawn>().Respawn();
  }
}