using System.Dynamic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : FlippableObject {
  
  [SerializeField] private float acceleration, jumpHeight;
  public float maxSpeed;
  [SerializeField, Range(0,1)] private float drag;
  private float speed;
  [HideInInspector] public bool grounded;
  private SpriteRenderer sprite;
  
  #region Controls

  [SerializeField] private InputActionReference jump, move, flip;
  public static int dir;
  
  public delegate void Notify();
  public delegate void NotifyIndexed(int i);
  public delegate void NotifyBool(bool i);
  public static event NotifyIndexed PlayerJumped;
  public static event NotifyIndexed PlayerMoved;
  public static event NotifyBool PlayerFlipped;
  public static event Notify PlayerLanded;
  
  private void OnEnable() {
    jump.action.performed += OnJump;
    move.action.performed += OnMove;
    flip.action.performed += OnFlip;
    move.action.canceled += OnMove;
  }

  private void OnDisable() {
    jump.action.performed -= OnJump;
    move.action.performed -= OnMove;
    flip.action.performed -= OnFlip;
    move.action.canceled -= OnMove;
  }
  public void Landed() => PlayerLanded?.Invoke();
  
  private void OnJump(InputAction.CallbackContext ctx) {
    if (!grounded) return;
    rb.linearVelocity = new Vector3(rb.linearVelocity.x, (flipped ? -1 : 1) * jumpHeight);
    grounded = false;
    PlayerJumped?.Invoke(dir);
  }
  private void OnMove(InputAction.CallbackContext ctx) {
    float horizontal = ctx.ReadValue<float>();
    dir = horizontal switch {
      >= .25f => 1,
      <= -.25f => -1,
      _ => 0
    };
    if (dir == 0) return;
    sprite.flipX = dir < 0;
    PlayerMoved?.Invoke(dir);
    if (!grounded) return;
  }
  private void OnFlip(InputAction.CallbackContext ctx) {
    if (!canFlip || !flipOver || PlayerPickupController.carryingSomething) return; 
    PlayerFlipped?.Invoke(flipped);
    Flip(transform.localScale.y);
  }
  
  #endregion
  
  protected override void Start() {
    base.Start();
    rb = GetComponent<Rigidbody2D>();
    sprite = GetComponent<SpriteRenderer>();
  }
  
  private void FixedUpdate() => Movement();

  private void Movement() {
    if (!flipOver) speed = 0;
    else speed = Mathf.Clamp(speed + dir * acceleration, -maxSpeed, maxSpeed) * (1 - drag);
    Vector2 targetVel = Vector2.right * speed;
    targetVel.y = ApplyGravity(rb.linearVelocity.y);
    rb.linearVelocity = targetVel;
  }


  
}