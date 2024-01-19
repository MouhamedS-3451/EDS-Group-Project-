using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InteractableFindRoom : Interactable
{

  public Tilemap tilemap;
    // Start is called before the first frame update
    void Start()
    {
      tilemap.color = new Color(1, 1, 1, 1);
    }

  public override void InRangeAction()
  {
    tilemap.color = new Color(1, 1, 1, 0.75f);
    gameObject.SetActive(true);
  }
}
