using UnityEngine;

public class Movement : MonoBehaviour
{
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

  void Awake()
  {
    body = GetComponent<Rigidbody2D>();
    sprite = GetComponentInChildren<SpriteRenderer>();
  }

  void Update()
  {
    float direction = Input.GetAxisRaw("Horizontal");

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

    Lean(velocity);
    body.velocity = new Vector2(velocity, body.velocity.y);
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
    if (!useDeceleration)
    {
      return 0;
    }
    speed -= deceleration * Time.deltaTime;
    if (speed < 0) speed = 0;

    return speed;
  }

  // Leans Forward when moving
  void Lean(float velocity)
  {
    if (!useLean) return;
    if (velocity < 0) transform.eulerAngles = Vector3.forward * leanDegree;
    else if (velocity > 0) transform.eulerAngles = Vector3.forward * leanDegree * -1;
    else transform.eulerAngles = Vector3.forward * 0;
  }
}