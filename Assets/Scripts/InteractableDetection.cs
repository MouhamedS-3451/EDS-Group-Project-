using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDetection : MonoBehaviour
{
  [SerializeField] private LayerMask interactableLayer;
  GameObject interactable;
  private bool inRange = false;

  void Update()
  {
    // Call Interact()  if player is in range and presses interact key
    if (inRange && Input.GetKeyDown(KeyCode.E))
    {
      interactable.GetComponent<Interactable>().Interact();
    }

  }

  // Call InRangeAction() if player enters range of object
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other != null && ((((1 << other.gameObject.layer) & interactableLayer) != 0)))
    {
      inRange = true;
      other.gameObject.BroadcastMessage("InRangeAction");
      interactable = other.gameObject;
    }
  }

  // Call OutOfRangeAction() if player exits range of object
  private void OnTriggerExit2D(Collider2D other)
  {
    if (other != null && ((((1 << other.gameObject.layer) & interactableLayer) != 0)))
    {
      inRange = false;
      other.gameObject.BroadcastMessage("OutOfRangeAction");
      interactable = null;
    }
  }
}
