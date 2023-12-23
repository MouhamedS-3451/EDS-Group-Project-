using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWaterSource : Interactable
{
  [SerializeField] private GameObject player;

  void Awake()
  {
    InteractionKey = player.GetComponent<Inventory>().wateringCanKey;
  }

  public override void Interact()
  {
    if (!player.GetComponent<Inventory>().wateringCan) return;
    player.GetComponent<PlayerMovement>().LookAtTarget(transform.gameObject);
    player.GetComponent<Inventory>().FillWateringCan();
  }
}
