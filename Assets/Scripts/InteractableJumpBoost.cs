using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableJumpBoost : Interactable
{
  [SerializeField] GameObject player;
  [SerializeField] float jumpBoostMultiplier = 2f;
  [SerializeField] Sprite spriteDown;
  private Sprite spriteUp;

  public void Awake()
  {
    spriteUp = transform.GetComponentInParent<SpriteRenderer>().sprite;
    gameObject.SetActive(false);
  }

  public override void InRangeAction()
  {
    player.GetComponent<Jumping>().jumpHeightMultiplier = jumpBoostMultiplier;
    //transform.GetComponentInParent<SpriteRenderer>().enabled = false;
    transform.GetComponent<SpriteRenderer>().sprite = spriteDown;
  }

  public override void OutOfRangeAction()
  {
    player.GetComponent<Jumping>().jumpHeightMultiplier = 1f;
    transform.GetComponent<SpriteRenderer>().sprite = spriteUp;
  }
}
