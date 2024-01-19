using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTorch : Interactable
{
  [SerializeField] private GameObject player;

  void Start()
  {
    if (GameObject.Find("Player").GetComponent<Inventory>().torch)
      gameObject.SetActive(false);
  }


  public override void Interact()
  {
    player.GetComponent<PlayerMovement>().LookAtTarget(transform.gameObject);
    player.GetComponent<Inventory>().torch = true;
    gameObject.SetActive(false);
  }
}
