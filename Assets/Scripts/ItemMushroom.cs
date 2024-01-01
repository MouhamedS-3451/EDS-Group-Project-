using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMushroom : Interactable
{
  [SerializeField] private GameObject player;
  

  public override void Interact()
  {
    player.GetComponent<PlayerMovement>().LookAtTarget(transform.gameObject);
    player.GetComponent<Inventory>().mushroom = true;
    gameObject.SetActive(false);
  }
}
