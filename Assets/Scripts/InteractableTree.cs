using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class InteractableTree : Interactable
{
  [SerializeField] GameObject player;
  [SerializeField] Camera cam;
  bool isActive = true;
  public override void Interact()
  {
    foreach (Transform child in transform)
    {
      if (child.tag.CompareTo("Toggleable") == 1) continue;
      child.GetComponent<SpriteRenderer>().enabled = !isActive;
      child.GetComponent<BoxCollider2D>().enabled = !isActive;
    }
    isActive = !isActive;
    StartCoroutine(CameraFocusTree());
  }

  private IEnumerator CameraFocusTree()
  {
    //GameObject player = GameObject.Find("Player");
    //GameObject camera = GameObject.Find("Main Camera");
    float PosSmoothTime = cam.GetComponent<CameraFollow>().PosSmoothTime;
    float zoom = cam.GetComponent<Camera>().orthographicSize;

    cam.GetComponent<CameraZoom>().SetZoom(20f);
    cam.GetComponent<CameraFollow>().target = transform;
    cam.GetComponent<CameraFollow>().PosSmoothTime = 0.5f;
    player.GetComponent<Movement>().active = false;

    yield return new WaitForSeconds(2f);

    cam.GetComponent<CameraFollow>().target = player.transform;
    cam.GetComponent<CameraZoom>().SetZoom(zoom);

    yield return new WaitForSeconds(1f);
    cam.GetComponent<CameraFollow>().PosSmoothTime = PosSmoothTime;
    player.GetComponent<Movement>().active = true;
  }
}
