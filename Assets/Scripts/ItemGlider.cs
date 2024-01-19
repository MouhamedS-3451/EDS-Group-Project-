using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGlider : Interactable
{
  [SerializeField] private GameObject player;
  public GameObject barrier;

  void Start()
  {
    if (player.GetComponent<Inventory>().glider)
      gameObject.SetActive(false);
  }

  public override void Interact()
  {
    player.GetComponent<PlayerMovement>().LookAtTarget(transform.gameObject);
    player.GetComponent<Inventory>().glider = true;
    gameObject.SetActive(false);
    if (barrier != null) barrier.SetActive(false);
  }
}
