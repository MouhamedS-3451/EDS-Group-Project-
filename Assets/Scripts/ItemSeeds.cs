using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ItemSeeds : Interactable
{
  [SerializeField] private GameObject player;
  public override void Interact()
  {
    player.GetComponent<PlayerMovement>().LookAtTarget(transform.gameObject);
    player.GetComponent<Inventory>().seeds = true;
    gameObject.SetActive(false);
  }
}
