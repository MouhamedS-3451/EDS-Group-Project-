using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlatform : Interactable
{
  public MovingPlatform platform;
  public override void InRangeAction()
  {
    platform.speed = 0;
    platform.transform.position = platform.locations[platform.startIndex].position;
    platform.SetIndex(platform.startIndex);

  }
}
