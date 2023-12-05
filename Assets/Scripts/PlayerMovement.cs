using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{
  public bool active = true;
  public float maxSpeed = 20f;
  public bool useAcceleration = true;
  public float acceleration = 50f;
  public bool useDeceleration = true;
  public float deceleration = 100f;
  public bool useLean = true;
  public float leanDegree = 2.5f;

  private Rigidbody2D body;
  private SpriteRenderer sprite;
  private float SpeedLeft = 0f;
  private float SpeedRight = 0f;
  private float direction = 0f;
  private float lastPosition;

  void Awake()
  {
    body = GetComponent<Rigidbody2D>();
    sprite = GameObject.Find("PlayerSpriteRenderer").GetComponent<SpriteRenderer>();
    lastPosition = body.position.x;
  }

  void FixedUpdate()
  {

    if (Input.GetAxisRaw("Vertical") == -1)
    {
      GameObject ground = transform.GetComponentInChildren<PlayerGroundDetection>().ground;
      Debug.Log(ground);
      if (ground != null && ground.layer == 7)
      {
        StartCoroutine(DeactivatePlatform(ground));
      }

    }

    direction = Input.GetAxisRaw("Horizontal");
    if (!active) direction = 0;

    // Determine which direction to accelerate in
    // Direction is 1 if moving right, -1 if moving left, 0 if not moving
    switch (direction)
    {
      case 1:
        sprite.flipX = false;
        SpeedRight = Accelerate(SpeedRight);
        SpeedLeft = Decelerate(SpeedLeft);
        break;
      case -1:
        sprite.flipX = true;
        SpeedLeft = Accelerate(SpeedLeft);
        SpeedRight = Decelerate(SpeedRight);
        break;
      case 0:
        SpeedLeft = Decelerate(SpeedLeft);
        SpeedRight = Decelerate(SpeedRight);
        break;
    }

    float velocity = -1 * SpeedLeft + SpeedRight;
    body.velocity = new Vector2(velocity, body.velocity.y);
    if (useLean) Lean();

    lastPosition = body.position.x;
  }

  // Linear acceleration
  // Repeatedly adds acceleration value to speed
  float Accelerate(float speed)
  {
    if (!useAcceleration)
    {
      return maxSpeed;
    }
    speed += acceleration * Time.deltaTime;
    if (speed > maxSpeed) speed = maxSpeed;

    return speed;
  }

  // Linear deceleration
  // Repeatedly subtracts deceleration value from speed
  float Decelerate(float speed)
  {
    if (!useDeceleration) return 0;

    speed -= deceleration * Time.deltaTime;
    if (speed < 0) speed = 0;

    return speed;
  }

  // Leans Forward when moving
  void Lean()
  {
    float current_speed = (body.position.x - lastPosition) / Time.deltaTime;

    if (current_speed != 0) sprite.transform.eulerAngles = Vector3.forward * leanDegree * -1 * direction;
    else sprite.transform.eulerAngles = Vector3.forward * 0;
  }

  public float GetDirection()
  {
    return direction;
  }
  bool IsGrounded()
  {
    if (!active) return false;
    return transform.GetComponentInChildren<PlayerGroundDetection>().IsGrounded();
  }

  IEnumerator DeactivatePlatform(GameObject ground)
  {
    ground.GetComponent<Collider2D>().enabled = false;
    yield return new WaitForSeconds(0.5f);
    ground.GetComponent<Collider2D>().enabled = true;
  }

}