using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCheckpoint : Interactable
{
  [SerializeField] private GameObject player;
  [SerializeField] private int checkpointID;
  [SerializeField][ColorUsage(true, true)] private Color colorInactive;
  [SerializeField][ColorUsage(true, true)] private Color colorActive;

  public void Awake()
  {
    GetComponent<SpriteRenderer>().material.SetColor("_Color", colorInactive);
  }
  public override void InRangeAction()
  {
    if (player.GetComponent<PlayerRespawn>().currentCheckpoint <= checkpointID)
    {
      GetComponent<SpriteRenderer>().material.SetColor("_Color", colorActive);
      player.GetComponent<PlayerRespawn>().currentCheckpoint = checkpointID;
      player.GetComponent<PlayerRespawn>().SetRespawnPoint(transform);
    }
  }
}
