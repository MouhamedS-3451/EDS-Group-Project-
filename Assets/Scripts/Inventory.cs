using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
  [SerializeField] private GameObject hotbar;
  [SerializeField] private GameObject collectiblesCounter;
  public int collectiblesTypeCount = 2;
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

  public bool[] collectiblesType1;
  public bool[] collectiblesType2;

  private AudioManager audioManager;

  public void Awake()
  {
    for (int i = 0; i < collectiblesTypeCount; i++)
    {
      foreach (Transform collectible in collectiblesCounter.transform.GetChild(i))
      {
        Color color = Color.black;
        color.a = 0.5f;
        collectible.GetComponent<Image>().color = color;
      }
    }

    torchActive = false;

    audioManager = FindObjectOfType<AudioManager>();

  }

  void Update()
  {
    HideShowItems();

    hotbar.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().fillAmount = waterLevel;

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
    if (!wateringCan || waterLevel == 1) return;
    waterLevel = 1;
    water = true;
    audioManager.Play("FillWater");
  }

  public void UseWateringCan(float time, bool keepWater = false)
  {
    if (!water) return;
    StartCoroutine(UseWateringCanCoroutine(time, keepWater));
  }

  private IEnumerator UseWateringCanCoroutine(float time, bool keepWater)
  {
    audioManager.Play("UseWater", gameObject);
    transform.GetComponent<PlayerMovement>().active = false;
    transform.GetComponent<PlayerJumping>().active = false;
    wateringCanActive = true;
    

    yield return new WaitForSeconds(time);

    transform.GetComponent<PlayerMovement>().active = true;
    transform.GetComponent<PlayerJumping>().active = true;
    wateringCanActive = false;
    if (!keepWater) water = false;
    //audioManager.Stop("UseWater", gameObject);
  }

  public void UseTorch()
  {
    if (!torch) return;

    if (torchActive)
    {
      torchActive = false;
      audioManager.Stop("TorchLighting", gameObject);
      audioManager.Stop("TorchBurning", gameObject);
    }
    else
    {
      audioManager.Play("TorchLighting", gameObject);
      audioManager.Play("TorchBurning", gameObject);
      torchActive = true;
    }
  }

  public void UseGlider()
  {
    if (!glider) return;
    gliderActive = !gliderActive;
  }

  public void Collect(int type, int index)
  {
    Transform image = collectiblesCounter.transform.GetChild(type-1).GetChild(index);
    Transform animator = image.transform.GetChild(0);
    if (type == 1) collectiblesType1[index] = true;
    else if (type == 2) collectiblesType2[index] = true;

    image.GetComponent<Image>().color = Color.white;
    
    animator.gameObject.SetActive(true);
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
