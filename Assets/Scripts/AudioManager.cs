using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
  public Sound[] sounds;

  public static AudioManager instance;
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
    if (source == null) source = sourceObject.AddComponent<AudioSource>();

    // Assign AudioSource properties
    source.clip = sound.clip;
    source.volume = sound.volume;
    source.pitch = sound.pitch;
    source.spatialBlend = sound.spatialBlend;
    source.loop = sound.loop;

    source.Play();
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