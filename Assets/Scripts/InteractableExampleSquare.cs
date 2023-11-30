using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableExampleSquare : Interactable
{
  public override void InRangeAction()
  {
    Debug.Log("In Range of square");
  }

  public override void OutOfRangeAction()
  {
    Debug.Log("Out of Range of square");
  }

  public override void Interact()
  {
    Debug.Log("Interacted with square");
  }
}