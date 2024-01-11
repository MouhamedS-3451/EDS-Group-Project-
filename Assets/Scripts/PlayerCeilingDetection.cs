using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCeilingDetection : MonoBehaviour
{
  [SerializeField] private LayerMask ceilingLayer;
  private bool hasCeiling;

  private void OnTriggerStay2D(Collider2D collider)
  {
    if (collider != null && (((1 << collider.gameObject.layer) & ceilingLayer) != 0))
    {
      hasCeiling = true;
    }
  }

  private void OnTriggerExit2D(Collider2D collider)
  {
    if (collider != null && (((1 << collider.gameObject.layer) & ceilingLayer) != 0))
    {
      hasCeiling = false;
    }
  }

  public bool HasCeiling()
  {
    return hasCeiling;
  }
}
