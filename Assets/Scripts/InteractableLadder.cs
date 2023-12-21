using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableLadder : Interactable
{
  public GameObject player;
  
  public override void InRangeAction()
  {
    player.GetComponent<PlayerMovement>().SetOnLadder(true);
  }
  public override void OutOfRangeAction()
  {
    player.GetComponent<PlayerMovement>().SetOnLadder(false);
  }
}
