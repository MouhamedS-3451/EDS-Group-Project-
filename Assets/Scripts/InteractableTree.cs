using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTree : Interactable
{
  public override void Interact()
  {
    Debug.Log("Interacting with tree");
    GameObject[] foliage = GameObject.FindGameObjectsWithTag("Toggleable");

    foreach (GameObject f in foliage)
    {
      f.SetActive(false);
    }
  }
}
