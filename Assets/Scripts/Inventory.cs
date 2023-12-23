using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  [SerializeField] private GameObject hotbar;
  public KeyCode wateringCanKey = KeyCode.Alpha1;
  public KeyCode torchKey = KeyCode.Alpha2;
  public KeyCode mushroomKey = KeyCode.Alpha3;
  public KeyCode seedsKey = KeyCode.Alpha4;
  public KeyCode gliderKey = KeyCode.LeftShift;

  public bool wateringCan = false;
  public bool water = false;
  public float waterLevel = 0;
  public bool torch = false;
  public bool mushroom = false;
  public bool seeds = false;
  public bool glider = false;  

  void Awake()
  {  
    DeactivateHotbarItems();
    DeactivatePlayerItems();
  }

  void Update()
  {
    hotbar.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<UnityEngine.UI.Image>().fillAmount = waterLevel;
    
    if (Input.GetKeyDown(torchKey)) EquipTorch();
    if (Input.GetKeyDown(gliderKey)) EquipGlider();
  }

  public void PickUpWateringCan()
  {
    hotbar.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    wateringCan = true;
    
  }
  public void FillWateringCan()
  {
    if (!wateringCan || water) return;
    waterLevel = 1;
    water = true;
  }

  public void UseWateringCan(float time)
  {
    if (!water) return;
    StartCoroutine(UseWateringCanCoroutine(time));
  }

  private IEnumerator UseWateringCanCoroutine(float time)
  {
    transform.GetComponent<PlayerMovement>().active = false;
    transform.Find("Items").Find("WateringCan").GetComponent<SpriteRenderer>().enabled = true;

    yield return new WaitForSeconds(time);

    transform.GetComponent<PlayerMovement>().active = true;
    transform.Find("Items").Find("WateringCan").GetComponent<SpriteRenderer>().enabled = false;
    water = false;
  }

  public void PickUpTorch()
  {
    hotbar.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
    torch = true;
  }

  public void EquipTorch()
  {
    if (!torch) return;
    // TODO: Show torch/ Hide torch
  }

  public void PickUpMushroom()
  {
    hotbar.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
    mushroom = true;
  }

  public void PickUpSeeds()
  {
    hotbar.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
    seeds = true;
  }

  public void PickUpGlider()
  {
    hotbar.transform.GetChild(4).GetChild(0).gameObject.SetActive(true);
    glider = true;
  }

  public void EquipGlider()
  {
    if (!glider) return;
    // TODO: Show glider/ Hide glider
  }

  void DeactivateHotbarItems()
  {
    foreach (Transform child in hotbar.transform)
    {
      child.GetChild(0).gameObject.SetActive(false);
    }
  }

  void DeactivatePlayerItems()
  {
    transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
  }

}
