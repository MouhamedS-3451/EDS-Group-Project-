using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCollectible : Interactable
{
  [SerializeField] GameObject player;
  [SerializeField] int collectibleType;
  [SerializeField] int collectibleIndex;
  [SerializeField] float growStart = 0;
  [SerializeField] float growEnd = 1;
  [SerializeField] float wateringTime = 1f;
  [SerializeField] GameObject forceField;
  private bool isCollected = false;


  public void Awake()
  {
    transform.GetChild(0).GetComponent<SpriteRenderer>().material = GetComponent<SpriteRenderer>().material;
    InteractionKey = player.GetComponent<Inventory>().wateringCanKey;
    transform.GetChild(0).GetComponent<SpriteRenderer>().material.SetFloat("_Cutoff_Height", growStart);
    forceField.SetActive(false);
  }
  public override void Interact()
  {
    if (!player.GetComponent<Inventory>().water || isCollected) return;

    player.GetComponent<Inventory>().UseWateringCan(wateringTime, true);
    LeanTween.value(gameObject, growStart, growEnd, wateringTime).setOnUpdate(UpdateGrowth);
    GetComponentInChildren<ParticleSystem>().Stop();
    forceField.SetActive(true);

    player.GetComponent<Inventory>().Collect(collectibleType, collectibleIndex);

    isCollected = true;
  }

  void UpdateGrowth(float value)
  {
    transform.GetChild(0).GetComponent<SpriteRenderer>().material.SetFloat("_Cutoff_Height", value);
  }
}
