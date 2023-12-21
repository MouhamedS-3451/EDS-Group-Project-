using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSeeds : Interactable
{
  [SerializeField] private float growthMin = 0;
  [SerializeField] private float growthMax = 1;
  private float growthCurrent;
  [SerializeField] private float growTime = 2.5f;
  [SerializeField] private GameObject ladderInteractable;
  //[SerializeField] private Material windEffect;
  private GameObject player;
  private bool isPlanted = false;
  private bool isGrowing = false;

  void Awake()
  {
    player = ladderInteractable.GetComponent<InteractableLadder>().player;
    InteractionKey = player.GetComponent<Inventory>().seedsKey;
    GetComponent<SpriteRenderer>().enabled = false;
    growthCurrent = growthMin;

    ladderInteractable.GetComponent<SpriteRenderer>().material.SetFloat("_Cutoff_Height", growthCurrent);
    ladderInteractable.GetComponent<BoxCollider2D>().enabled = false;
    ladderInteractable.SetActive(false);
  }

  void Update()
  {
    if (!isGrowing) return;
    GetComponent<Transition>().SmoothDamp(ref growthCurrent, growthMin, growthMax, growTime);
    ladderInteractable.GetComponent<SpriteRenderer>().material.SetFloat("_Cutoff_Height", growthCurrent);
    if (growthCurrent == growthMax)
    {
      isGrowing = false;
      gameObject.SetActive(false);
      //ladderInteractable.GetComponent<SpriteRenderer>().material = windEffect;
      ladderInteractable.GetComponent<BoxCollider2D>().enabled = true;
    }
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
      ladderInteractable.SetActive(true);
      isGrowing = true;
    }
  }
}
