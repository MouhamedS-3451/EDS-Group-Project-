using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTree : Interactable
{
  [SerializeField] GameObject player;
  [SerializeField] Camera cam;
  [SerializeField] Material foliageMaterial;
  [SerializeField] float bgStrength = 1;
  [SerializeField] private float zoom = 20f;
  [SerializeField] private float growTime = 10f;
  [SerializeField] private float wateringTime = 2.5f;
  [SerializeField] private float growthValue;
  [SerializeField] private float growthMax = 10f;
  private float growthMin = 0f;
  private bool isInteractable = true;
  private bool isGrowing = false;
  [SerializeField] private bool toggleInstant = false;
  [SerializeField] private List<GameObject> gameObjectsToToggle;
  private List<bool> gameObjectsStatus = new List<bool>();

  public void Awake()
  {
    InteractionKey = player.GetComponentInChildren<Inventory>().wateringCanKey;
    foreach (Transform child in transform)
    {
      if (child.tag.CompareTo("Toggleable") == 1) continue;
      child.GetComponent<SpriteRenderer>().material = foliageMaterial;
      child.GetComponent<SpriteRenderer>().material.SetFloat("_Cutoff_Height", growthValue);

      if (child.gameObject.layer == LayerMask.NameToLayer("PlatformLeaves"))
      {
        child.GetComponent<BoxCollider2D>().enabled = false;
      }
      else
      {
        child.GetComponent<SpriteRenderer>().material.SetFloat("_Strength", bgStrength);
      }
    }

    foreach (GameObject gameObject in gameObjectsToToggle)
    {
      gameObjectsStatus.Add(gameObject.activeSelf);
    }
  }

  // Update updates the growth value over time
  public void Update()
  {
    if (!isGrowing) return;

    transform.GetComponent<Transition>().SmoothDamp(ref growthValue, growthMin, growthMax, growTime);
    player.GetComponentInChildren<Inventory>().waterLevel = 1 - growthValue;
    if (growthValue == growthMax) isGrowing = false;
    foreach (Transform child in transform)
    {
      if (child.tag.CompareTo("Toggleable") == 1) continue;
      child.GetComponent<SpriteRenderer>().material.SetFloat("_Cutoff_Height", growthValue);
    }


    if (toggleInstant) return;
    for (int i = 0; i < gameObjectsToToggle.Count; i++)
    {
      gameObjectsToToggle[i].SetActive(true);
      try
      {
        Color spriteColor = gameObjectsToToggle[i].GetComponentInChildren<SpriteRenderer>(includeInactive: true).color;

        if (gameObjectsStatus[i]) spriteColor.a = 1 - growthValue;
        else spriteColor.a = growthValue;

        gameObjectsToToggle[i].GetComponentInChildren<SpriteRenderer>().color = spriteColor;
      }
      catch { }

    }
  }

  public override void Interact()
  {
    if (!isInteractable || player.GetComponentInChildren<Inventory>().waterLevel != 1) return;

    player.GetComponent<PlayerMovement>().LookAtTarget(transform.gameObject);
    player.GetComponentInChildren<Inventory>().UseWateringCan(wateringTime);
    isGrowing = true;
    isInteractable = false;
    StartCoroutine(Coroutine());
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


    for (int i = 0; i < gameObjectsToToggle.Count; i++)
    {
      gameObjectsToToggle[i].SetActive(!gameObjectsStatus[i]);
    }


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
