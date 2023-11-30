using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableJumpBoost : Interactable
{
  [SerializeField] float jumpBoostMultiplier = 2f;

  public override void InRangeAction()
  {
    GameObject.Find("Player").GetComponent<Jumping>().jumpHeightMultiplier = jumpBoostMultiplier;
  }
  public override void OutOfRangeAction()
  {
    GameObject.Find("Player").GetComponent<Jumping>().jumpHeightMultiplier = 1f;
  }
}
