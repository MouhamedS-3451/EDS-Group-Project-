using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
  private Transform respawnPoint;
  public int currentCheckpoint = 0;
  public float delay = 1f;
  public float respawnDelay = 1f;
  [SerializeField] new GameObject camera;

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
    
    yield return new WaitForSeconds(delay);

    transform.position = respawnPoint.position;

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
