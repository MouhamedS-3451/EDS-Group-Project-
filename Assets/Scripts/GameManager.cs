using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;

  public int currentLevel = 0;
  public int unlockedLevel = 0;

  public bool[] unlockedItems = new bool[5];

  public bool[] collectiblesLvl1_1 = new bool[0];
  public bool[] collectiblesLvl1_2 = new bool[0];
  public bool[] collectiblesLvl2_1 = new bool[3];
  public bool[] collectiblesLvl2_2 = new bool[4];
  public bool[] collectiblesLvl3_1 = new bool[0];
  public bool[] collectiblesLvl3_2 = new bool[0];

  public float musicVolume;
  public float sfxVolume;

  void Awake()
  {
    if (instance == null)
    {
      instance = this;
    }
    else
    {
      Destroy(gameObject);
      return;
    }
    DontDestroyOnLoad(gameObject);

    LoadPrefs();


  }

  void LoadPrefs()
  {
    // Progress
    if (!PlayerPrefs.HasKey("UnlockedLevel")) PlayerPrefs.SetInt("UnlockedLevel", 0);
    unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel");

    for (int i = 0; i < unlockedItems.Length; i++)
    {
      if (!PlayerPrefs.HasKey("UnlockedItem" + i)) PlayerPrefs.SetInt("UnlockedItem" + i, 0);
      unlockedItems[i] = PlayerPrefs.GetInt("UnlockedItem" + i) == 1;
    }

    // Audio
    if (!PlayerPrefs.HasKey("VolumeMusic")) PlayerPrefs.SetFloat("VolumeMusic", 0.75f);
    musicVolume = PlayerPrefs.GetFloat("VolumeMusic");
    Debug.Log(PlayerPrefs.GetFloat("VolumeMusic", 0.75f));

    if (!PlayerPrefs.HasKey("VolumeSFX")) PlayerPrefs.SetFloat("VolumeSFX", 0.75f);
    sfxVolume = PlayerPrefs.GetFloat("VolumeSFX");

    // Collectibles
    for (int i = 0; i < collectiblesLvl1_1.Length; i++)
    {
      if (!PlayerPrefs.HasKey("CollectibleLvl1_1_" + i)) PlayerPrefs.SetInt("CollectibleLvl1_1_" + i, 0);
      collectiblesLvl1_1[i] = PlayerPrefs.GetInt("CollectibleLvl1_1_" + i) == 1;
    }

    for (int i = 0; i < collectiblesLvl1_2.Length; i++)
    {
      if (!PlayerPrefs.HasKey("CollectibleLvl1_2_" + i)) PlayerPrefs.SetInt("CollectibleLvl1_2_" + i, 0);
      collectiblesLvl1_2[i] = PlayerPrefs.GetInt("CollectibleLvl1_2_" + i) == 1;
    }

    for (int i = 0; i < collectiblesLvl2_1.Length; i++)
    {
      if (!PlayerPrefs.HasKey("CollectibleLvl2_1_" + i)) PlayerPrefs.SetInt("CollectibleLvl2_1_" + i, 0);
      collectiblesLvl2_1[i] = PlayerPrefs.GetInt("CollectibleLvl2_1_" + i) == 1;
    }

    for (int i = 0; i < collectiblesLvl2_2.Length; i++)
    {
      if (!PlayerPrefs.HasKey("CollectibleLvl2_2_" + i)) PlayerPrefs.SetInt("CollectibleLvl2_2_" + i, 0);
      collectiblesLvl2_2[i] = PlayerPrefs.GetInt("CollectibleLvl2_2_" + i) == 1;
    }

    for (int i = 0; i < collectiblesLvl3_1.Length; i++)
    {
      if (!PlayerPrefs.HasKey("CollectibleLvl3_1_" + i)) PlayerPrefs.SetInt("CollectibleLvl3_1_" + i, 0);
      collectiblesLvl3_1[i] = PlayerPrefs.GetInt("CollectibleLvl3_1_" + i) == 1;
    }

    for (int i = 0; i < collectiblesLvl3_2.Length; i++)
    {
      if (!PlayerPrefs.HasKey("CollectibleLvl3_2_" + i)) PlayerPrefs.SetInt("CollectibleLvl3_2_" + i, 0);
      collectiblesLvl3_2[i] = PlayerPrefs.GetInt("CollectibleLvl3_2_" + i) == 1;
    }
  }

  public void UpdatePrefs()
  {
    // Progress
    PlayerPrefs.SetInt("UnlockedLevel", unlockedLevel);

    for (int i = 0; i < unlockedItems.Length; i++)
    {
      PlayerPrefs.SetInt("UnlockedItem" + i, unlockedItems[i] ? 1 : 0);
    }

    // Audio
    PlayerPrefs.SetFloat("VolumeMusic", musicVolume);
    PlayerPrefs.SetFloat("VolumeSFX", sfxVolume);

    // Collectibles
    for (int i = 0; i < collectiblesLvl1_1.Length; i++)
    {
      PlayerPrefs.SetInt("CollectibleLvl1_1_" + i, collectiblesLvl1_1[i] ? 1 : 0);
    }

    for (int i = 0; i < collectiblesLvl1_2.Length; i++)
    {
      PlayerPrefs.SetInt("CollectibleLvl1_2_" + i, collectiblesLvl1_2[i] ? 1 : 0);
    }

    for (int i = 0; i < collectiblesLvl2_1.Length; i++)
    {
      PlayerPrefs.SetInt("CollectibleLvl2_1_" + i, collectiblesLvl2_1[i] ? 1 : 0);
    }

    for (int i = 0; i < collectiblesLvl2_2.Length; i++)
    {
      PlayerPrefs.SetInt("CollectibleLvl2_2_" + i, collectiblesLvl2_2[i] ? 1 : 0);
    }

    for (int i = 0; i < collectiblesLvl3_1.Length; i++)
    {
      PlayerPrefs.SetInt("CollectibleLvl3_1_" + i, collectiblesLvl3_1[i] ? 1 : 0);
    }

    for (int i = 0; i < collectiblesLvl3_2.Length; i++)
    {
      PlayerPrefs.SetInt("CollectibleLvl3_2_" + i, collectiblesLvl3_2[i] ? 1 : 0);
    }
  }

  public void ResetPrefs()
  {
    PlayerPrefs.DeleteAll();
    LoadPrefs();
  }

  public void LoadMenu()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
  }


  void OnApplicationQuit()
  {
    UpdatePrefs();

  }

}
