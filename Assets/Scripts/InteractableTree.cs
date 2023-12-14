using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor.Animations;
using UnityEngine;

public class InteractableTree : Interactable
{
  [SerializeField] GameObject player;
  [SerializeField] Camera cam;
  [SerializeField] Material foliageMaterial;
  [SerializeField] float bgStrength = 1;
  [SerializeField] private float zoom = 20f;
  [SerializeField] private float growSpeed = 10f;
  // Growth goes from 0 to 1
  [SerializeField] private float growthValue = 0f;
  [SerializeField] private float growthMax = 1f;
  private bool isInteractable = true;
  private bool isGrowing = false;

  public override void Interact()
  {
    // TODO: Check if player is allowed to interact
    if (!isInteractable) return;
    isGrowing = true;
    isInteractable = false;
    StartCoroutine(Coroutine());
  }

  public void Awake()
  {
    foreach (Transform child in transform)
    {
      if (child.tag.CompareTo("Toggleable") == 1) continue;
      child.GetComponent<SpriteRenderer>().material = foliageMaterial;
      child.GetComponent<SpriteRenderer>().material.SetFloat("_Cutoff_Height", growthValue);

      if (child.gameObject.layer == LayerMask.NameToLayer("Platform"))
      {
        child.GetComponent<BoxCollider2D>().enabled = false;
      }
      else
      {
        child.GetComponent<SpriteRenderer>().material.SetFloat("_Strength", bgStrength);
      }
    }
  }
  // Update just gradually updates the growth value
  public void Update()
  {
    if (!isGrowing) return;

    growthValue += (Time.deltaTime / growSpeed);
    if (growthValue >= growthMax)
    {
      isGrowing = false;
      growthValue = growthMax;
    }
    foreach (Transform child in transform)
    {
      if (child.tag.CompareTo("Toggleable") == 1) continue;
      child.GetComponent<SpriteRenderer>().material.SetFloat("_Cutoff_Height", growthValue);
    }
  }

  private IEnumerator Coroutine()
  {
    float initialPosSmoothTime = cam.GetComponent<CameraFollow>().PosSmoothTime;
    float initialZoom = cam.GetComponent<Camera>().orthographicSize;

    // Pan camera to tree
    cam.GetComponent<CameraZoom>().SetZoom(zoom);
    cam.GetComponent<CameraFollow>().target = transform;
    cam.GetComponent<CameraFollow>().PosSmoothTime = 0.5f;

    while (isGrowing) yield return null;

    // Pan camera back to player
    cam.GetComponent<CameraFollow>().target = player.transform;
    cam.GetComponent<CameraZoom>().SetZoom(initialZoom);

    ActivatePlatforms();

    yield return new WaitForSeconds(1f);

    cam.GetComponent<CameraFollow>().PosSmoothTime = initialPosSmoothTime;
  }

  private void ActivatePlatforms()
  {
    foreach (Transform child in transform)
    {
      if (child.tag.CompareTo("Toggleable") == 1) continue;
      if (child.gameObject.layer != LayerMask.NameToLayer("Platform")) continue;
      isGrowing = true;
      child.GetComponent<BoxCollider2D>().enabled = true;
    }
  }
}
