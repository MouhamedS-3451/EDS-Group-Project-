using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWaterSource : Interactable
{
  [SerializeField] private GameObject player;

  void Awake()
  {
    InteractionKey = player.GetComponent<Inventory>().bucketKey;
  }

  public override void Interact()
  {
    if (!player.GetComponent<Inventory>().bucket) return;
    player.GetComponent<PlayerMovement>().LookAtTarget(transform.gameObject);
    player.GetComponent<Inventory>().FillBucket();
  }
}
