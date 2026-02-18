using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {
  // Animator variables stored as something less expensive to search for
  private static readonly int Grounded = Animator.StringToHash("Grounded");
  private static readonly int XVel = Animator.StringToHash("XVel");
  private static readonly int YVel = Animator.StringToHash("YVel");
  
  [SerializeField] private float acceleration, maxSpeed, jumpHeight, gravity;
  [SerializeField, Range(0,1)] private float drag;
  
  private bool reversed;
  private float speed;
  [HideInInspector] public bool grounded;
  
  #region Controls

  [SerializeField] private InputActionReference jump, move;
  private int dir;
  
  private void OnEnable() {
    jump.action.performed += OnJump;
    move.action.performed += OnMove;
    move.action.canceled += OnMove;
  }

  private void OnDisable() {
    jump.action.performed -= OnJump;
    move.action.performed -= OnMove;
    move.action.canceled -= OnMove;
  }

  public delegate void Notify();
  public event Notify PlayerJumped;
  private void OnJump(InputAction.CallbackContext ctx) {
    if (!grounded) return;
    rb.linearVelocity += (reversed ? Vector2.down : Vector2.up) * jumpHeight;
    grounded = false;
    poof.transform.localScale = new Vector3(dir < 0 ? 1 : -1, 1, 1);
    poof.Emit(1);
    // the ground check is subscribed to this,
    // it tells it to keep grounded false so the player can't double jump
    PlayerJumped?.Invoke();
  }
  
  private void OnMove(InputAction.CallbackContext ctx) {
    float direction = ctx.ReadValue<Vector2>().x;
    dir = direction switch {
      >= .25f => 1,
      <= -.25f => -1,
      _ => 0
    };
    if (dir == 0) return;
    sprite.flipX = dir < 0;
    if (!grounded) return;
    poof.transform.localScale = new Vector3(dir < 0 ? 1 : -1, 1, 1);
    poof.Emit(1);
  }

  #endregion
  
  private Rigidbody2D rb;
  private SpriteRenderer sprite;
  private Animator anim;
  private ParticleSystem poof;

  private void Start() {
    rb = GetComponent<Rigidbody2D>();
    sprite = GetComponent<SpriteRenderer>();
    anim = GetComponent<Animator>();
    poof = GetComponentInChildren<ParticleSystem>();
  }
  
  private void FixedUpdate() {
    Movement();
    Animation();
  }

  private void Movement() {
    speed = Mathf.Clamp(speed + dir * acceleration, -maxSpeed, maxSpeed) * (1 - drag);

    Vector2 targetVel = Vector2.right * speed;
    targetVel.y += (reversed ? 1 : -1) * gravity + rb.linearVelocity.y;

    rb.linearVelocity = targetVel;
  }

  private void Animation() {
    anim.SetBool(Grounded, grounded);
    anim.SetFloat(XVel, Mathf.Abs(rb.linearVelocity.x) / maxSpeed * 4);
    anim.SetFloat(YVel, rb.linearVelocity.y);
  }

  public void EmitLandingPoof() {
    poof.transform.localScale = new Vector3(1, 1, 1);
    poof.Emit(1);
    poof.transform.localScale = new Vector3(-1, 1, 1);
    poof.Emit(1);
  }
}