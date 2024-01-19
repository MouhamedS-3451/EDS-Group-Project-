using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ItemSeeds : Interactable
{
  [SerializeField] private GameObject player;

  void Start()
  {
    if (player.GetComponent<Inventory>().seeds)
      gameObject.SetActive(false);
  }
  
  public override void Interact()
  {
    player.GetComponent<PlayerMovement>().LookAtTarget(transform.gameObject);
    player.GetComponent<Inventory>().seeds = true;
    gameObject.SetActive(false);
  }
}
