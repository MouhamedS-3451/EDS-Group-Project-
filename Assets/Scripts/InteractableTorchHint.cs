using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTorchHint : Interactable
{
  public GameObject player;
  public GameObject speechbubble;

  public GameObject barrier; 

  void Start()
  {
    speechbubble.SetActive(false);
  }

  void Update()
  {
    if (player.GetComponent<Inventory>().torch)
      barrier.SetActive(false);
  }
  

  public override void InRangeAction()
  {
    if (!player.GetComponent<Inventory>().torch)
    {
      speechbubble.SetActive(true);
    }
    
  }

  public override void OutOfRangeAction()
  {
    speechbubble.SetActive(false);
  }
}
