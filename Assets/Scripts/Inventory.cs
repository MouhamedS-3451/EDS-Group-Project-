using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
  [SerializeField] private GameObject hotbar;
  public KeyCode bucketKey = KeyCode.Alpha1;
  public KeyCode torchKey = KeyCode.Alpha2;
  public KeyCode mushroomKey = KeyCode.Alpha3;
  public KeyCode seedsKey = KeyCode.Alpha4;
  public KeyCode gliderKey = KeyCode.LeftShift;

  public bool bucket = false;
  public bool water = false;
  public bool torch = false;
  public bool mushroom = false;
  public bool seeds = false;
  public bool glider = false;

  [SerializeField] private Sprite bucketFilled;
  private Sprite bucketEmpty;

  void Awake()
  {
    bucketEmpty = hotbar.transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite;
    hotbar.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    hotbar.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
    hotbar.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
    hotbar.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
    hotbar.transform.GetChild(4).GetChild(0).gameObject.SetActive(false);
    transform.Find("Bucket").GetComponent<SpriteRenderer>().enabled = false;
  }

  void Update()
  {
    if (Input.GetKeyDown(torchKey)) EquipTorch();
    if (Input.GetKeyDown(gliderKey)) EquipGlider();
  }

  public void PickUpBucket()
  {
    hotbar.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
    bucket = true;
    
  }
  public void FillBucket()
  {
    if (!bucket) return;
    hotbar.transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = bucketFilled;
    water = true;
  }

  public void UseBucket(float time)
  {
    if (!water) return;
    StartCoroutine(UseBucketCoroutine(time));
  }

  private IEnumerator UseBucketCoroutine(float time)
  {
    transform.GetComponent<PlayerMovement>().active = false;
    transform.Find("Bucket").GetComponent<SpriteRenderer>().enabled = true;

    yield return new WaitForSeconds(time);

    transform.Find("Bucket").GetComponent<SpriteRenderer>().enabled = false;
    transform.GetComponent<PlayerMovement>().active = true;
    hotbar.transform.GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = bucketEmpty;
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
}
