using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

public class PlayerRespawn : MonoBehaviour
{
  private Transform respawnPoint;
  public int currentCheckpoint = 0;
  public float delay = 1f;
  public float respawnDelay = 1f;
  public float dissolveSpeed = 1f;
  private bool fadingOut = false;
  private bool fadingIn = false;

  float fade = 1f;
  [SerializeField] new GameObject camera;

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
    Debug.Log(fade);
    transform.Find("PlayerSpriteRenderer").GetComponent<SpriteRenderer>().material.SetFloat("_Fade", fade);
    //GetComponentInChildren<PlayerSpriteRenderer>().material.SetFloat("_Fade", fade);
  }
  public void Respawn()
  {
    StartCoroutine(RespawnCoroutine());
  }

  private IEnumerator RespawnCoroutine()
  {
    float deceleration = GetComponent<Movement>().deceleration;
    GetComponent<Movement>().deceleration = 25f;
    GetComponent<Movement>().active = false;
    GetComponent<Jumping>().active = false;
    fadingOut = true;

    yield return new WaitForSeconds(delay);

    transform.position = respawnPoint.position;
    fadingIn = true;

    yield return new WaitForSeconds(respawnDelay);

    GetComponent<Movement>().deceleration = deceleration;
    GetComponent<Movement>().active = true;
    GetComponent<Jumping>().active = true;

  }

  public void SetRespawnPoint(Transform point)
  {
    respawnPoint = point;
  }
}
