using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableJumpBoost : Interactable
{
  [SerializeField] GameObject player;
  [SerializeField] float jumpBoostMultiplier = 2f;
  [SerializeField] private Sprite spriteDown;
  private Sprite spriteUp;

  public void Awake()
  {
    spriteUp = transform.GetComponent<SpriteRenderer>().sprite;
    gameObject.SetActive(false);
  }

  public override void InRangeAction()
  {
    player.GetComponent<PlayerJumping>().jumpHeightMultiplier = jumpBoostMultiplier;
    //transform.GetComponentInParent<SpriteRenderer>().enabled = false;
    transform.GetComponent<SpriteRenderer>().sprite = spriteDown;
  }

  public override void OutOfRangeAction()
  {
    player.GetComponent<PlayerJumping>().jumpHeightMultiplier = 1f;
    transform.GetComponent<SpriteRenderer>().sprite = spriteUp;
  }
}
