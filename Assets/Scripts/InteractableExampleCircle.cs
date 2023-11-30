using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableExampleCircle : Interactable
{
  public override void InRangeAction()
  {
    Debug.Log("In Range of circle");
  }

  public override void OutOfRangeAction()
  {
    Debug.Log("Out of Range of circle");
  }

  public override void Interact()
  {
    Debug.Log("Interacted with circle");
  }
}