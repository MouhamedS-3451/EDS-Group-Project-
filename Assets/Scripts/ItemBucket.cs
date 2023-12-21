using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBucket : Interactable
{
  [SerializeField] private GameObject player;
  [SerializeField] private GameObject tooltip;
  public override void Interact()
  {
    player.GetComponent<PlayerMovement>().LookAtTarget(transform.gameObject);
    player.GetComponent<Inventory>().PickUpBucket();
    gameObject.SetActive(false);
    OutOfRangeAction();
  }
  public override void InRangeAction()
  {
    tooltip.GetComponent<Text>().text = "Pick up";
    tooltip.SetActive(true);
  }
  public override void OutOfRangeAction()
  {
    tooltip.GetComponent<Text>().text = "";
    tooltip.SetActive(false);
  }
}
