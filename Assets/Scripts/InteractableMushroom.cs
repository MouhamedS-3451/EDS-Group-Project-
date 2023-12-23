using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableMushroom : Interactable
{
  [SerializeField] private GameObject player;
  [SerializeField] private float growTime = 2.5f;
  [SerializeField] private GameObject JumpBoostInteractable;
  //private bool isInteractable = true;
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
    InteractionKey = player.GetComponent<Inventory>().mushroomKey;
    InteractionKey = KeyCode.Alpha3;
  }

  // Update mushroom scale
  public void Update()
  {
    if (!isGrowing) return;
    transform.GetComponent<Transition>().SmoothDamp(ref currentSize, minSize, maxSize, growTime);
    player.GetComponent<Inventory>().waterLevel = 1 - currentSize;
    if (currentSize == maxSize) isGrowing = false;
    transform.GetComponentInChildren<SpriteRenderer>().transform.localScale = new Vector3(currentSize, currentSize, currentSize);
  }

  public override void Interact()
  {
    if (!player.GetComponent<Inventory>().mushroom) return;
    player.GetComponent<PlayerMovement>().LookAtTarget(transform.gameObject);

    // Plant mushroom
    if (!isPlanted && player.GetComponent<Inventory>().mushroom)
    {
      transform.GetComponentInChildren<SpriteRenderer>().enabled = true;
      InteractionKey = player.GetComponent<Inventory>().mushroomKey;
      InteractionKey = KeyCode.Alpha1;
      isPlanted = true;
    }
    // Water mushroom
    else if(isPlanted && player.GetComponent<Inventory>().water)
    {
      player.GetComponent<Inventory>().UseWateringCan(growTime);
      isGrowing = true;
      //isInteractable = false;
      StartCoroutine(Deactivate());
    }
  }

  private IEnumerator Deactivate()
  {
    yield return new WaitForSeconds(growTime);
    gameObject.SetActive(false);
    JumpBoostInteractable.SetActive(true);
  }
}
