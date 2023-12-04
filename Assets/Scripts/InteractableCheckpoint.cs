using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCheckpoint : Interactable
{
  [SerializeField] private int checkpointID;
  public override void InRangeAction()
  {    
    
    GameObject player = GameObject.Find("Player");

    if (player.GetComponent<PlayerRespawn>().currentCheckpoint < checkpointID)
    {
      GetComponent<SpriteRenderer>().color = Color.green;
      player.GetComponent<PlayerRespawn>().currentCheckpoint = checkpointID;
      player.GetComponent<PlayerRespawn>().SetRespawnPoint(transform);
    }
    
  }
}
