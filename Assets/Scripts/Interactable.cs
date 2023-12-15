using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]

// Base class for all interactable objects
// Interactable objects need to have Layer set to "Interactable"
public abstract class Interactable : MonoBehaviour
{
  public KeyCode InteractionKey { get; set; } = KeyCode.E;

  // Gets called once when player gets in range of object
  public virtual void InRangeAction() { }

  // Gets called once when player gets out of range of object
  public virtual void OutOfRangeAction() { }

  // Gets called every time player presses interact button while in range
  public virtual void Interact() { }
}