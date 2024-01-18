using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
  private GameManager gameManager;
  Transform level1;
  Transform level2;
  Transform level3;

  void Start()
  {
    gameManager = FindObjectOfType<GameManager>();

    level1 = GameObject.Find("Level1").transform;
    level2 = GameObject.Find("Level2").transform;
    level3 = GameObject.Find("Level3").transform;

    // Lock or unlock levels based on unlockedLevel
    if (gameManager.unlockedLevel >= 1) level1.transform.GetChild(5).gameObject.SetActive(false);
    if (gameManager.unlockedLevel >= 2) level2.transform.GetChild(5).gameObject.SetActive(false);
    if (gameManager.unlockedLevel >= 3) level3.transform.GetChild(5).gameObject.SetActive(false);

    SetCollectibleStatus();



  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      gameManager.LoadLevel("MainMenu");
    }
  }

  public void LoadLevel(string levelName)
  {
    gameManager.LoadLevel(levelName);
  }

  private void SetCollectibleStatus()
  {
    // Set collectibles based on collectibles arrays
    for (int i = 0; i < gameManager.collectiblesLvl1_1.Length; i++)
    {
      SetCollectibleColor(level1.transform.GetChild(0).GetChild(i), gameManager.collectiblesLvl1_1[i]);
      
    }
    for (int i = 0; i < gameManager.collectiblesLvl1_2.Length; i++)
    {
      SetCollectibleColor(level1.transform.GetChild(1).GetChild(i), gameManager.collectiblesLvl1_2[i]);
    }

    for (int i = 0; i < gameManager.collectiblesLvl2_1.Length; i++)
    {
      SetCollectibleColor(level2.transform.GetChild(0).GetChild(i), gameManager.collectiblesLvl2_1[i]);
    }
    for (int i = 0; i < gameManager.collectiblesLvl2_2.Length; i++)
    {
      SetCollectibleColor(level2.transform.GetChild(1).GetChild(i), gameManager.collectiblesLvl2_2[i]);
    }

    for (int i = 0; i < gameManager.collectiblesLvl3_1.Length; i++)
    {
      SetCollectibleColor(level3.transform.GetChild(0).GetChild(i), gameManager.collectiblesLvl3_1[i]);
    }
    for (int i = 0; i < gameManager.collectiblesLvl3_2.Length; i++)
    {
      SetCollectibleColor(level3.transform.GetChild(1).GetChild(i), gameManager.collectiblesLvl3_2[i]);
    }
  }

  private void SetCollectibleColor(Transform collectible, bool collected)
  {
    if (collected)
    {
      collectible.GetComponent<Image>().color = Color.white;
    }
    else
    {
      Color color = new Color(0.25f, 0.25f, 0.25f, 1f);
      collectible.GetComponent<Image>().color = Color.black;
    }
  }


}
