using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public bool active = true;
  public float maxSpeed = 20f;
  public float climbSpeed = 10f;
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
  private float directionX = 0f;
  private float directionY = 0f;
  private float lastPosition;
  private bool onLadder = false;
  public float gravityScaleStart = 15f;
  public float gravityScaleGlider = 0.5f;
  public float gravityScale;

  void Awake()
  {
    gravityScale = gravityScaleStart;
    body = GetComponent<Rigidbody2D>();
    sprite = GameObject.Find("PlayerSpriteRenderer").GetComponent<SpriteRenderer>();
    lastPosition = body.position.x;
  }

  void FixedUpdate()
  {
    if (active)
    {
      directionX = Input.GetAxisRaw("Horizontal");
      directionY = Input.GetAxisRaw("Vertical");
    }
    else
    {
      directionX = 0;
      directionY = 0;
    }

    // Determine which direction to accelerate in
    // Direction is 1 if moving right, -1 if moving left, 0 if not moving
    switch (directionX)
    {
      case 0:
        SpeedLeft = Decelerate(SpeedLeft);
        SpeedRight = Decelerate(SpeedRight);
        break;
      case 1:
        ChangeFacingDirection(directionX);
        SpeedRight = Accelerate(SpeedRight);
        SpeedLeft = Decelerate(SpeedLeft);
        break;
      case -1:
        ChangeFacingDirection(directionX);
        SpeedLeft = Accelerate(SpeedLeft);
        SpeedRight = Decelerate(SpeedRight);
        break;
    }

    GameObject ground = transform.GetComponentInChildren<PlayerGroundDetection>().ground;
    if (ground != null && ground.layer == 7 && directionY == -1)
    {
      StartCoroutine(FallThroughPlatform(ground));
    }

    if (onLadder && !IsJumping() && (!IsGrounded() || directionY != 0))
    {
      body.velocity = new Vector2(body.velocity.x, directionY * climbSpeed);
      body.gravityScale = 0;
    }
    else
    {
      body.velocity = new Vector2(body.velocity.x, body.velocity.y);
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

  private void ChangeFacingDirection(float direction)
  {
    float x = transform.localScale.x;
    float y = transform.localScale.y;
    float z = transform.localScale.z;
    if (direction == 1) x = Mathf.Abs(transform.localScale.x);
    else if (direction == -1) x = -1 * Mathf.Abs(transform.localScale.x);

    transform.localScale = new Vector3(x, y, z);
  }

  public void LookAtTarget(GameObject target, float distanceTreshold = 0f)
  {
    float Xdistance = target.transform.position.x - transform.position.x;
    if (Mathf.Abs(Xdistance) < distanceTreshold) return;

    if (Xdistance > 0) ChangeFacingDirection(1);
    else if (Xdistance < 0) ChangeFacingDirection(-1);
  }
  // Leans Forward when moving
  void Lean()
  {
    float current_speed = (body.position.x - lastPosition) / Time.deltaTime;

    if (current_speed != 0) sprite.transform.eulerAngles = -1 * directionX * leanDegree * Vector3.forward;
    else sprite.transform.eulerAngles = Vector3.forward * 0;
  }

  public float GetDirectionX()
  {
    return directionX;
  }

  public void SetOnLadder(bool onLadder)
  {
    this.onLadder = onLadder;
    if (!onLadder) body.gravityScale = gravityScale;
  }

  bool IsGrounded()
  {
    if (!active) return false;
    return transform.GetComponentInChildren<PlayerGroundDetection>().IsGrounded();
  }

  bool IsJumping()
  {
    return transform.GetComponentInChildren<PlayerJumping>().IsJumping();
  }

  IEnumerator FallThroughPlatform(GameObject ground)
  {
    ground.GetComponent<Collider2D>().enabled = false;
    yield return new WaitForSeconds(0.5f);
    ground.GetComponent<Collider2D>().enabled = true;
  }

}