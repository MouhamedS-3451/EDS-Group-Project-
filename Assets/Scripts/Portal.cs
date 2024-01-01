using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Interactable
{
  [SerializeField] private GameObject player;
  [SerializeField] private GameObject destination;
  void Start()
  {
    InteractionKey = KeyCode.W;
  }

  public override void Interact()
  {
    player.transform.position = destination.transform.position;
  }
}
