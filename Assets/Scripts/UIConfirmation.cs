using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConfirmation : MonoBehaviour
{
  public void Cancel()
  {
    gameObject.SetActive(false);
  }
}
