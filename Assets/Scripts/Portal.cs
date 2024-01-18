using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Portal : Interactable
{
  [SerializeField]private bool walkIn = false;
  [SerializeField] private KeyCode key = KeyCode.W;
  [SerializeField] private GameObject player;
  [SerializeField] private GameObject destination;
  private bool inRange = false;

  void Start()
  {
    InteractionKey = key;
  }
  void Update()
  {
    if (inRange && Input.GetKeyDown(KeyCode.UpArrow)) Interact();
  }

  public override void InRangeAction()
  {
    inRange = true;
    if (walkIn) Teleport();
  }
  public override void OutOfRangeAction()
  {
    inRange = false;
  }

  public override void Interact()
  {
    if (!walkIn) Teleport();
    OutOfRangeAction();
  }

  void Teleport()
  {
    player.transform.position = destination.transform.position;
    player.GetComponent<Inventory>().torchActive = player.transform.position.y < 175;
  }
}
