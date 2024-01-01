using UnityEngine;

public class InteractableDropItems : Interactable
{
  [SerializeField] private GameObject player;
  public override void InRangeAction()
  {
    player.GetComponent<Inventory>().wateringCan = false;
    player.GetComponent<Inventory>().water = false;
    player.GetComponent<Inventory>().waterLevel = 0;
    player.GetComponent<Inventory>().torchActive = false;
    player.transform.position = transform.position;
    transform.GetChild(0).gameObject.SetActive(true);
  }
}
