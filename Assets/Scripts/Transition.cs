using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
  // Updates current position to end position over time
  // Does not overshoot
  public void SmoothDamp(ref float current, float start, float end, float time)
  {
    current += Time.deltaTime * (end - start) / time;
    if (current > end) current = end;
  }
}
