using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class InteractableDetection : MonoBehaviour
{
  [SerializeField] private LayerMask interactableLayer;
  GameObject interactable;

  void Update()
  {
    // Call Interact()  if player is in range and presses interact key
    if (interactable != null && Input.GetKeyDown(GetKey()))
    {
      interactable.GetComponent<Interactable>().Interact();
    }

  }

  // Call InRangeAction() if player enters range of object
  private void OnTriggerStay2D(Collider2D other)
  {
    if (other != null && (((1 << other.gameObject.layer) & interactableLayer) != 0) && interactable == null)
    {
      other.gameObject.BroadcastMessage("InRangeAction", SendMessageOptions.DontRequireReceiver);
      interactable = other.gameObject;
    }
  }

  // Call OutOfRangeAction() if player exits range of object
  private void OnTriggerExit2D(Collider2D other)
  {
    //if (other != null && ((((1 << other.gameObject.layer) & interactableLayer) != 0)))
    if (other != null && other.gameObject == interactable)
    {
      other.gameObject.BroadcastMessage("OutOfRangeAction", SendMessageOptions.DontRequireReceiver);
      interactable = null;
    }
  }

  private KeyCode GetKey()
  {
    return interactable.GetComponent<Interactable>().InteractionKey;
  }
}
