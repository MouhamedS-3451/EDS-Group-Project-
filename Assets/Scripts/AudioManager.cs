using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
  //public static AudioManager instance;

  public GameManager gameManager;

  public Sound[] sounds;

  public AudioMixer audioMixer;

  [HideInInspector]
  public AudioMixerGroup mixerGroupMaster;

  [HideInInspector]
  public AudioMixerGroup mixerGroupMusic;

  [HideInInspector]
  public AudioMixerGroup mixerGroupSFX;


  
  void Start()
  {
    gameManager = FindObjectOfType<GameManager>();

    mixerGroupMaster = audioMixer.FindMatchingGroups("Master")[0];
    mixerGroupMusic = audioMixer.FindMatchingGroups("Music")[0];
    mixerGroupSFX = audioMixer.FindMatchingGroups("SFX")[0];
    
    audioMixer.SetFloat("VolumeMusic", Mathf.Log10(gameManager.musicVolume) * 20);
    audioMixer.SetFloat("VolumeSFX", Mathf.Log10(gameManager.sfxVolume) * 20);
  }

  public void Play(string name) { Play(name, gameObject); }

  public void Play(string name, GameObject sourceObject)
  {

    Sound sound = Array.Find(sounds, sound => sound.name == name);
    if (sound == null)
    {
      Debug.LogWarning("Sound: " + name + " not found!");
      return;
    }

    // Find existing AudioSource with the same clip or create a new one
    AudioSource source = null;
    foreach (AudioSource child in sourceObject.GetComponents<AudioSource>())
    {
      if (child.clip == sound.clip) source = child;
    }
    if (source == null)
    {
      source = sourceObject.AddComponent<AudioSource>();
    }

    // Assign/Update AudioSource properties
    source.clip = sound.clip;
    source.volume = sound.volume;
    source.pitch = sound.pitch;
    source.spatialBlend = sound.spatialBlend;
    source.loop = sound.loop;
    source.outputAudioMixerGroup = sound.type == Sound.SoundType.Music ? mixerGroupMusic : mixerGroupSFX;

    if (!source.isPlaying) source.Play();
  }

  public void Stop(string name) { Stop(name, gameObject); }

  public void Stop(string name, GameObject sourceObject)
  {

    Sound sound = Array.Find(sounds, sound => sound.name == name);
    if (sound == null)
    {
      Debug.LogWarning("Sound: " + name + " not found!");
      return;
    }

    AudioSource source = null;
    foreach (AudioSource child in sourceObject.GetComponents<AudioSource>())
    {
      if (child.clip == sound.clip) source = child;
    }
    if (source == null) return;

    source.Stop();
  }
}
