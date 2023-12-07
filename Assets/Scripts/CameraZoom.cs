using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
  [SerializeField] private Camera cam;
  public float zoom;
  public float zoomSmoothTime = 0.25f;
  private float velocity;

  void Start()
  {
    zoom = cam.orthographicSize;
  }

  void Update()
  {
    cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, zoomSmoothTime);
  }

  public void SetZoom(float zoom)
  {
    this.zoom = zoom;
  }

}
