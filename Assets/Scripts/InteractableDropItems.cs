using UnityEngine;

public class InteractableDropItems : Interactable
{
  [SerializeField] private GameObject player;
  public override void InRangeAction()
  {
    player.GetComponent<Inventory>().wateringCan = false;
    player.GetComponent<Inventory>().water = true;
    player.GetComponent<Inventory>().waterLevel = 0.25f;
    player.GetComponent<Inventory>().torchActive = false;
    player.transform.position = transform.position;
    transform.GetChild(0).gameObject.SetActive(true);
  }
}
