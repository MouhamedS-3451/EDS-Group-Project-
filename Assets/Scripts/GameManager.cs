using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
  public static GameManager instance;

  public string currentLevel = "MainMenu";
  public int unlockedLevel = 1;

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
    if (!PlayerPrefs.HasKey("UnlockedLevel")) PlayerPrefs.SetInt("UnlockedLevel", 1);
    unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel");

    for (int i = 0; i < unlockedItems.Length; i++)
    {
      if (!PlayerPrefs.HasKey("UnlockedItem" + i)) PlayerPrefs.SetInt("UnlockedItem" + i, 0);
      unlockedItems[i] = PlayerPrefs.GetInt("UnlockedItem" + i) == 1;
    }

    // Audio
    if (!PlayerPrefs.HasKey("VolumeMusic")) PlayerPrefs.SetFloat("VolumeMusic", 0.75f);
    musicVolume = PlayerPrefs.GetFloat("VolumeMusic");

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

  public void ResetProgress()
  {
    float musicVolume = PlayerPrefs.GetFloat("VolumeMusic");
    float sfxVolume = PlayerPrefs.GetFloat("VolumeSFX");
    PlayerPrefs.DeleteAll();
    PlayerPrefs.SetFloat("VolumeMusic", musicVolume);
    PlayerPrefs.SetFloat("VolumeSFX", sfxVolume);
    LoadPrefs();
  }

  public void LoadScene(int level)
  {
    switch (level)
    {
      case 1:
        StartCoroutine(LoadLevelCoroutine("Level 1"));
        break;
      case 2:
        StartCoroutine(LoadLevelCoroutine("Level 2"));
        break;
      case 3:
        StartCoroutine(LoadLevelCoroutine("Level 3"));
        break;
      default:
        StartCoroutine(LoadLevelCoroutine("LevelSelect"));
        break;
    }
  }

  public void LoadLevel(string levelName)
  {
    StartCoroutine(LoadLevelCoroutine(levelName));
  }

  private IEnumerator LoadLevelCoroutine(string levelName)
  {


    var asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(levelName);
    while (!asyncLoad.isDone)
    {
      yield return null;
    }

    AudioManager audioManager = FindObjectOfType<AudioManager>();
    audioManager.audioMixer.SetFloat("VolumeMusic", Mathf.Log10(musicVolume) * 20);
    audioManager.audioMixer.SetFloat("VolumeSFX", Mathf.Log10(sfxVolume) * 20);

    switch (levelName)
    {
      case "Level 1":
        LoadLevel1(audioManager);
        break;
      case "Level 2":
        LoadLevel2(audioManager);
        break;
      case "Level 3":
        LoadLevel3(audioManager);
        break;
      case "MainMenu":
        LoadMainMenu(audioManager);
        break;
      case "LevelSelect":
        LoadLevelSelect(audioManager);
        break;
      case "Credits":
        LoadCredits(audioManager);
        break;


    }
  }

  private void LoadLevel1(AudioManager audioManager)
  {

    audioManager.Play("ThemeLvl1");
    currentLevel = "Level 1";
    Inventory inv = LoadInventory();
    Cursor.visible = false;
  }
  private void LoadLevel2(AudioManager audioManager)
  {
    audioManager.Play("ThemeLvl2");
    currentLevel = "Level 2";
    Inventory inv = LoadInventory();
    Cursor.visible = false;
  }

  private void LoadLevel3(AudioManager audioManager)
  {
    audioManager.Play("ThemeLvl3");
    currentLevel = "Level 3";
    Inventory inv = LoadInventory();
    Cursor.visible = false;
  }

  private void LoadMainMenu(AudioManager audioManager)
  {
    audioManager.Play("ThemeMainMenu");
    Cursor.visible = true;
  }

  private void LoadLevelSelect(AudioManager audioManager)
  {
    audioManager.Play("ThemeLevelSelect");
    Cursor.visible = true;
  }

  private void LoadCredits(AudioManager audioManager)
  {
    audioManager.Play("ThemeCredits");
    Cursor.visible = true;
  }

  private Inventory LoadInventory()
  {
    GameObject player = GameObject.Find("Player");
    Inventory inv = player.GetComponent<Inventory>();
    inv.wateringCan = unlockedItems[0];
    inv.torch = unlockedItems[1];
    inv.mushroom = unlockedItems[2];
    inv.seeds = unlockedItems[3];
    inv.glider = unlockedItems[4];

    return inv;
  }



  public void LevelComplete()
  {
    // Fade out music
    // Fade out color
    Inventory inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    unlockedLevel++;
    if (unlockedLevel > 3) unlockedLevel = 3;

    unlockedItems[0] = inv.wateringCan;
    unlockedItems[1] = inv.torch;
    unlockedItems[2] = inv.mushroom;
    unlockedItems[3] = inv.seeds;
    unlockedItems[4] = inv.glider;

    switch (currentLevel)
    {
      case "Level 1":
        for (int i = 0; i < collectiblesLvl1_1.Length; i++)
        {
          collectiblesLvl1_1[i] = inv.collectiblesType1[i];
        }
        for (int i = 0; i < collectiblesLvl1_2.Length; i++)
        {
          collectiblesLvl1_2[i] = inv.collectiblesType2[i];
        }
        break;
      case "Level 2":
        for (int i = 0; i < collectiblesLvl2_1.Length; i++)
        {
          collectiblesLvl2_1[i] = inv.collectiblesType1[i];
        }
        for (int i = 0; i < collectiblesLvl2_2.Length; i++)
        {
          collectiblesLvl2_2[i] = inv.collectiblesType2[i];
        }
        break;
      case "Level 3":
        for (int i = 0; i < collectiblesLvl3_1.Length; i++)
        {
          collectiblesLvl3_1[i] = inv.collectiblesType1[i];
        }
        for (int i = 0; i < collectiblesLvl3_2.Length; i++)
        {
          collectiblesLvl3_2[i] = inv.collectiblesType2[i];
        }
        break;
      default:
        break;
    }

    UpdatePrefs();
  }



  void OnApplicationQuit()
  {
    Debug.Log("SavedOnQuit");
    UpdatePrefs();

  }

}
