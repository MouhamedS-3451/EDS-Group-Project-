using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemWateringCan : Interactable
{
  [SerializeField] private GameObject player;
  public override void Interact()
  {
    player.GetComponent<PlayerMovement>().LookAtTarget(transform.gameObject);
    player.GetComponent<Inventory>().PickUpWateringCan();
    gameObject.SetActive(false);
    OutOfRangeAction();
  }
}
