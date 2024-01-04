using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Portal : Interactable
{
  [SerializeField]private bool walkIn = false;
  [SerializeField] private KeyCode key = KeyCode.W;
  [SerializeField] private GameObject player;
  [SerializeField] private GameObject destination;

  void Start()
  {
    InteractionKey = key;
  }

  public override void InRangeAction()
  {
    if (walkIn) Teleport();
  }

  public override void Interact()
  {
    if (!walkIn) Teleport();
  }

  void Teleport()
  {
    player.transform.position = destination.transform.position;
    player.GetComponent<Inventory>().torchActive = player.transform.position.y < 175;
  }
}
