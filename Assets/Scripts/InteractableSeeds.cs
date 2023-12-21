using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSeeds : Interactable
{
  [SerializeField] private float growthMin = 0;
  [SerializeField] private float growthMax = 1;
  private float growthValue;
  [SerializeField] private float growTime = 2.5f;
  [SerializeField] private GameObject ladderInteractable;
  private GameObject player;
  private bool isPlanted = false;
  private bool isGrowing = false;
  
  void Awake()
  {
    player = ladderInteractable.GetComponent<InteractableLadder>().player;
    growthValue = growthMin;
    transform.GetComponent<SpriteRenderer>().enabled = false;
    InteractionKey = player.GetComponent<Inventory>().seedsKey;
    ladderInteractable.SetActive(false);
  }

  void Update()
  {
    // TODO: gradually grow vines
  }

  public override void Interact()
  {
    player.GetComponent<PlayerMovement>().LookAtTarget(transform.gameObject);
    if (!isPlanted && player.GetComponent<Inventory>().seeds)
    {
      transform.GetComponent<SpriteRenderer>().enabled = true;
      InteractionKey = player.GetComponent<Inventory>().bucketKey;
      isPlanted = true;
      return;
    }
    if (isPlanted && player.GetComponent<Inventory>().water)
    {
      player.GetComponent<Inventory>().UseBucket(growTime);
      gameObject.SetActive(false);
      ladderInteractable.SetActive(true);
    }
  }
}
