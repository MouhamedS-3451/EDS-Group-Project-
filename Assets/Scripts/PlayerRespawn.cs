using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

public class PlayerRespawn : MonoBehaviour
{
  private Transform respawnPoint;
  public int currentCheckpoint = 0;
  public float delay = 1f;
  public float respawnDelay = 1f;
  private bool fadingOut = false;
  private bool fadingIn = false;

  float fade = 1f;

  public void Update()
  {
    if (fadingOut)
    {
      fade -= Time.deltaTime;
      if (fade < 0)
      {
        fade = 0;
        fadingOut = false;
      }
    }
    else if (fadingIn)
    {
      fade += Time.deltaTime;
      if (fade > 1)
      {
        fade = 1;
        fadingIn = false;
      }
    }
    transform.Find("PlayerSpriteRenderer").GetComponent<SpriteRenderer>().material.SetFloat("_Fade", fade);
  }
  public void Respawn()
  {
    StartCoroutine(RespawnCoroutine());
  }

  private IEnumerator RespawnCoroutine()
  {
    float deceleration = GetComponent<PlayerMovement>().deceleration;
    GetComponent<PlayerMovement>().deceleration = 25f;
    GetComponent<PlayerMovement>().active = false;
    GetComponent<Jumping>().active = false;
    fadingOut = true;

    yield return new WaitForSeconds(delay);

    transform.position = respawnPoint.position;
    fadingIn = true;

    yield return new WaitForSeconds(respawnDelay);

    GetComponent<PlayerMovement>().deceleration = deceleration;
    GetComponent<PlayerMovement>().active = true;
    GetComponent<Jumping>().active = true;

  }

  public void SetRespawnPoint(Transform point)
  {
    respawnPoint = point;
  }
}
