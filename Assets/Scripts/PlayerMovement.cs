using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
  public bool active = true;
  public float maxSpeed = 20f;
  public float climbSpeed = 10f;
  public float crouchSpeed = 10f;
  public bool useAcceleration = true;
  public float acceleration = 50f;
  public bool useDeceleration = true;
  public float deceleration = 100f;
  public bool useLean = true;
  public float leanDegree = 2.5f;

  private Rigidbody2D body;
  private SpriteRenderer sprite;
  private Animator animator;
  private float SpeedLeft = 0f;
  private float SpeedRight = 0f;
  private float directionX = 0f;
  private float directionY = 0f;
  private float lastPosition;
  private bool onLadder = false;
  public bool onWall = false;
  public LayerMask platformsLayerMask;
  private bool isCrouching = false;
  private float gravityScale;
  private AudioManager audioManager;
  public bool autoWalk = false;

  void Awake()
  {
    body = GetComponent<Rigidbody2D>();
    sprite = GameObject.Find("PlayerSpriteRenderer").GetComponent<SpriteRenderer>();
    lastPosition = body.position.x;
    gravityScale = body.gravityScale;
    transform.Find("PlayerHitboxCrouching").gameObject.SetActive(false);
    animator = transform.GetChild(1).GetComponent<Animator>();

    audioManager = FindObjectOfType<AudioManager>();
  }

  void FixedUpdate()
  {
    directionX = active ? Input.GetAxisRaw("Horizontal") : 0;
    directionY = active ? Input.GetAxisRaw("Vertical") : 0;

    if (autoWalk) directionX = 1;


    ChangeFacingDirection(directionX);

    UpdateXMovement();
    UpdateYMovement();

    if (useLean) Lean();

    SetAnimationStates();
  }

  private void UpdateXMovement()
  {
    // Determine which direction to accelerate in
    // Direction is 1 if moving right, -1 if moving left, 0 if not moving
    switch (directionX)
    {
      case 0:
        SpeedLeft = Decelerate(SpeedLeft);
        SpeedRight = Decelerate(SpeedRight);
        break;
      case 1:
        SpeedRight = Accelerate(SpeedRight);
        SpeedLeft = Decelerate(SpeedLeft);
        break;
      case -1:
        SpeedLeft = Accelerate(SpeedLeft);
        SpeedRight = Decelerate(SpeedRight);
        break;
    }

    // move left or right
    float velocity = -1 * SpeedLeft + SpeedRight;
    body.velocity = new Vector2(velocity, body.velocity.y);

    // Crouch
    if ((IsGrounded() && directionY == -1) || isCrouching && HasCeiling())
    {
      isCrouching = true;
      body.velocity = new Vector2(directionX * crouchSpeed, body.velocity.y);
      transform.Find("PlayerHitboxStanding").gameObject.SetActive(false);
      transform.Find("PlayerHitboxCrouching").gameObject.SetActive(true);
    }
    else
    {
      isCrouching = false;
      transform.Find("PlayerHitboxStanding").gameObject.SetActive(true);
      transform.Find("PlayerHitboxCrouching").gameObject.SetActive(false);
    }

    bool running = false;
    if (IsGrounded() && !IsCrouching() && directionX != 0) running = true;

    GameObject ground = transform.GetComponentInChildren<PlayerGroundDetection>().ground;

    if (ground == null) return;

    if (running && (ground.layer == 9 || ground.layer == 31)) audioManager.Play("FootstepsStone", gameObject);
    else audioManager.Stop("FootstepsStone", gameObject);

    if (running && (ground.layer == 10 || ground.layer == 30)) audioManager.Play("FootstepsGrass", gameObject);
    else audioManager.Stop("FootstepsGrass", gameObject);

  }

  private void UpdateYMovement()
  {
    GameObject ground = transform.GetComponentInChildren<PlayerGroundDetection>().ground;
    if (ground != null && (((1 << ground.layer) & platformsLayerMask) != 0) && directionY == -1)
    {
      StartCoroutine(FallThroughPlatform(ground));
    }

    // If on ladder, move up or down
    if (onLadder && !IsJumping() && (!IsGrounded() || directionY != 0))
    {
      body.velocity = new Vector2(body.velocity.x, directionY * climbSpeed);
      body.gravityScale = 0;

      if (directionY != 0) audioManager.Play("ClimbVine");
      else audioManager.Stop("ClimbVine");
    }
    else
    {
      body.velocity = new Vector2(body.velocity.x, body.velocity.y);

      audioManager.Stop("ClimbVine");
    }
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
    lastPosition = body.position.x;
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
    return transform.GetComponentInChildren<PlayerGroundDetection>().IsGrounded();
  }

  bool IsJumping()
  {
    return transform.GetComponentInChildren<PlayerJumping>().IsJumping();
  }

  bool IsCrouching()
  {
    return isCrouching;
  }

  bool HasCeiling()
  {
    return transform.GetComponentInChildren<PlayerCeilingDetection>().HasCeiling();
  }

  private void SetAnimationStates()
  {
    animator.SetBool("isRunning", Mathf.Abs(directionX) > 0);
    animator.SetBool("isJumping", IsJumping());
    animator.SetBool("isGrounded", IsGrounded());
    animator.SetBool("onLadder", onLadder);
    animator.SetBool("onWall", onWall);
    animator.SetBool("isCrouching", IsCrouching());
  }

  IEnumerator FallThroughPlatform(GameObject ground)
  {
    ground.GetComponent<Collider2D>().enabled = false;
    yield return new WaitForSeconds(0.5f);
    ground.GetComponent<Collider2D>().enabled = true;
  }
}