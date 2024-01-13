using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
  public string name;

  [HideInInspector]
  public AudioMixerGroup mixerGroup;

  public enum SoundType { SFX, Music }
  public SoundType type = SoundType.SFX;

  public AudioClip clip;

  [Range(0f, 1f)]
  public float volume = 1f;

  [Range(0, 3f)]
  public float pitch = 1f;

  [Range(0f, 1f)]
  public float spatialBlend = 0f;

  public bool loop = false;
}
