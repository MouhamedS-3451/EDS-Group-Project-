using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableMushroom : Interactable
{
  [SerializeField] private float growSpeed = 2.5f;
  [SerializeField] private GameObject JumpBoostInteractable;
  private bool isInteractable = true;
  private bool isPlanted = false;
  private bool isGrowing = false;
  private float currentSize;
  private float minSize;
  [SerializeField] private float maxSize = 5f;

  public void Awake()
  {
    minSize = transform.GetComponentInChildren<SpriteRenderer>().transform.localScale.x;
    currentSize = minSize;
    transform.GetComponentInChildren<SpriteRenderer>().enabled = false;
    //InteractionKey = Inventory.mushroom.key;
  }

  // Update mushroom scale
  public void Update()
  {
    if (!isGrowing) return;
    transform.GetComponent<Transition>().SmoothDamp(ref currentSize, minSize, maxSize, growSpeed);
    if (currentSize == maxSize) isGrowing = false;
    transform.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector3(currentSize, currentSize, currentSize);
  }

  public override void Interact()
  {
    if (!isInteractable) return;

    // Plant mushroom
    // TODO: Check if player has mushroom
    if (!isPlanted)
    {
      // If having mushrooms
      transform.GetComponentInChildren<SpriteRenderer>().enabled = true;
      //InteractionKey = Inverntory.bucket.key;
      isPlanted = true;
      return;
    }
    // Water mushroom
    // TODO: Check if player has water
    else
    {
      isGrowing = true;
      isInteractable = false;
      StartCoroutine(Deactivate());
    }
  }

  private IEnumerator Deactivate()
  {
    while (isGrowing) yield return null;
    gameObject.SetActive(false);
    JumpBoostInteractable.SetActive(true);
  }
}
