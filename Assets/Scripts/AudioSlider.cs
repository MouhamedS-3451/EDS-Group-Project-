using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
  GameManager gameManager;
  public AudioMixer audioMixer;
  private Slider musicSlider;
  private Slider sfxSlider;

  private float musicVolumePrev;
  private float sfxVolumePrev;
  private readonly float minValue = 0.0001f;

  // Start is called before the first frame update
  void Start()
  {
    gameManager = FindObjectOfType<GameManager>();

    musicSlider = GameObject.Find("Music").GetComponentInChildren<Slider>();
    sfxSlider = GameObject.Find("SFX").GetComponentInChildren<Slider>();

    musicSlider.value = gameManager.musicVolume;
    sfxSlider.value = gameManager.sfxVolume;
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SetMusicVolume(float volume)
  {
    if (gameManager == null) return;

    if (volume <= minValue)
    {
      volume = minValue;
      musicSlider.transform.parent.GetChild(1).GetChild(0).gameObject.SetActive(true);
    }
    else
    {
      musicSlider.transform.parent.GetChild(1).GetChild(0).gameObject.SetActive(false);
    }

    audioMixer.SetFloat("VolumeMusic", Mathf.Log10(volume) * 20);
    gameManager.musicVolume = volume;

  }

  public void SetSFXVolume(float volume)
  {
    if (gameManager == null) return;

    if (volume <= minValue)
    {
      volume = minValue;
      sfxSlider.transform.parent.GetChild(1).GetChild(0).gameObject.SetActive(true);
    }
    else sfxSlider.transform.parent.GetChild(1).GetChild(0).gameObject.SetActive(false);

    if (volume >= 0.33f) sfxSlider.transform.parent.GetChild(1).GetChild(1).gameObject.SetActive(true);
    else sfxSlider.transform.parent.GetChild(1).GetChild(1).gameObject.SetActive(false);

    if (volume >= 0.66f) sfxSlider.transform.parent.GetChild(1).GetChild(2).gameObject.SetActive(true);
    else sfxSlider.transform.parent.GetChild(1).GetChild(2).gameObject.SetActive(false);

    audioMixer.SetFloat("VolumeSFX", Mathf.Log10(volume) * 20);
    gameManager.sfxVolume = volume;

  }

  public void MuteMusic()
  {
    if (musicSlider.value > minValue)
    {
      musicVolumePrev = musicSlider.value;
      musicSlider.value = 0;
      SetMusicVolume(0);
    }
    else
    {
      if (musicVolumePrev <= minValue) musicVolumePrev = 0.75f;
      musicSlider.value = musicVolumePrev;
      SetMusicVolume(musicVolumePrev);
      musicVolumePrev = 0;
    }
  }

  public void MuteSFX()
  {
    if (sfxSlider.value > minValue)
    {
      sfxVolumePrev = sfxSlider.value;
      sfxSlider.value = 0;
      SetSFXVolume(0);
    }
    else
    {
      if (sfxVolumePrev <= minValue) sfxVolumePrev = 0.75f;
      sfxSlider.value = sfxVolumePrev;
      SetSFXVolume(sfxVolumePrev);
      sfxVolumePrev = 0;
    }
  }

}
