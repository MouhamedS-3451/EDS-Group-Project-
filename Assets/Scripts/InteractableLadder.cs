using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableLadder : Interactable
{
  public GameObject player;
  public bool wallLadder = false;
  
  public override void InRangeAction()
  {
    player.GetComponent<PlayerMovement>().SetOnLadder(true);
    if (wallLadder) player.GetComponent<PlayerMovement>().onWall = true;
  }
  public override void OutOfRangeAction()
  {
    player.GetComponent<PlayerMovement>().SetOnLadder(false);
    if (wallLadder) player.GetComponent<PlayerMovement>().onWall = false;
  }
}
