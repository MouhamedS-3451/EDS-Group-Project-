using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintController : MonoBehaviour
{
  [SerializeField] private GameObject torch;
  void Update()
  {
    foreach (Transform particleSystem in transform)
    {

      if (torch.activeSelf) particleSystem.gameObject.SetActive(false);
      else particleSystem.gameObject.SetActive(true);


    }
  }
}
