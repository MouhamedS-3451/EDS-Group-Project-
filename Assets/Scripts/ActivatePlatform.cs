using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlatform : Interactable
{
  public MovingPlatform platform;
  public override void InRangeAction()
  {
    platform.speed = 6;
  }
}
