using System.Collections;
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
  public bool wateringCanActive = false;
  public bool torch = false;
  public bool torchActive = false;
  public bool mushroom = false;
  public bool seeds = false;
  public bool glider = false;
  public bool gliderActive = false;

  void Update()
  {
    HideShowItems();

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

  public void UseTorch()
  {
    if (!torch) return;
    torchActive = !torchActive;
  }

  public void UseGlider()
  {
    if (!glider) return;
    gliderActive = !gliderActive;    
  }

  void HideShowItems()
  {
    hotbar.transform.GetChild(0).GetChild(0).gameObject.SetActive(wateringCan);
    hotbar.transform.GetChild(1).GetChild(0).gameObject.SetActive(torch);
    hotbar.transform.GetChild(2).GetChild(0).gameObject.SetActive(mushroom);
    hotbar.transform.GetChild(3).GetChild(0).gameObject.SetActive(seeds);
    hotbar.transform.GetChild(4).GetChild(0).gameObject.SetActive(glider);

    transform.GetChild(0).GetChild(0).gameObject.SetActive(wateringCanActive);
    transform.GetChild(0).GetChild(1).gameObject.SetActive(torchActive);
    transform.GetChild(0).GetChild(2).gameObject.SetActive(gliderActive);
  }

  bool IsGrounded()
  {
    return GetComponentInChildren<PlayerGroundDetection>().IsGrounded();
  }


}
