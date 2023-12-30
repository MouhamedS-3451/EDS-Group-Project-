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
  public bool gliderActive = false;

  void Awake()
  {
    HideHotbarItems();
    HidePlayerItems();
  }

  void Update()
  {
    hotbar.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<UnityEngine.UI.Image>().fillAmount = waterLevel;

    if (Input.GetKeyDown(torchKey)) UseTorch();
    if (Input.GetKeyDown(gliderKey)) UseGlider();
    if (Input.GetKeyUp(gliderKey)) UseGlider();
    if (gliderActive && !GetComponent<PlayerJumping>().isJumping)
    {
      GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
    }

    if (gliderActive && IsGrounded()) UseGlider();
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
    transform.GetChild(0).GetChild(0).gameObject.SetActive(true);

    yield return new WaitForSeconds(time);

    transform.GetComponent<PlayerMovement>().active = true;
    transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
    water = false;
  }

  public void PickUpTorch()
  {
    hotbar.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
    torch = true;
  }

  public void UseTorch()
  {
    if (!torch) return;
    bool status = transform.GetChild(0).GetChild(1).gameObject.activeSelf;
    transform.GetChild(0).GetChild(1).gameObject.SetActive(!status);
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

  public void UseGlider()
  {
    if (!glider) return;

    bool status = transform.GetChild(0).GetChild(2).gameObject.activeSelf;
    if (!status)
    {
      transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
      gliderActive = true;
    }
    else
    {
      transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
      gliderActive = false;
    }
  }

  void HideHotbarItems()
  {
    foreach (Transform hotbarItem in hotbar.transform)
    {
      hotbarItem.GetChild(0).gameObject.SetActive(false);
    }
  }

  void HidePlayerItems()
  {
    foreach (Transform playerItem in transform.GetChild(0))
    {
      playerItem.gameObject.SetActive(false);
    }
  }

  bool IsGrounded()
  {
    return GetComponentInChildren<PlayerGroundDetection>().IsGrounded();
  }


}
