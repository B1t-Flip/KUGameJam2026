using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationController : MonoBehaviour {
  // Animator variables stored as something less expensive to search for
  private static readonly int Grounded = Animator.StringToHash("Grounded");
  private static readonly int XVel = Animator.StringToHash("XVel");
  private static readonly int YVel = Animator.StringToHash("YVel");
  private static readonly int Pushing = Animator.StringToHash("Pushing");
  private Animator anim;
  private PlayerController player;
  private Rigidbody2D rb;
  
  private ParticleSystem poof;
  private ParticleSystem splash1;
  private ParticleSystem splash2;
  
  private void Awake() {
    anim = GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();
    player = GetComponent<PlayerController>();
    poof = GetComponentsInChildren<ParticleSystem>()[0];
    splash1 = GetComponentsInChildren<ParticleSystem>()[1];
    splash2 = GetComponentsInChildren<ParticleSystem>()[2];
  }

  private void OnEnable() {
    PlayerController.PlayerJumped += OnJump;
    PlayerController.PlayerFlipped += OnFlipped;
    PlayerController.PlayerMoved += OnMoved;
    PlayerController.PlayerLanded += OnLanded;
  }
  private void LateUpdate() {
    anim.SetBool(Grounded, player.grounded);
    anim.SetFloat(XVel, Mathf.Abs(rb.linearVelocity.x) / player.maxSpeed * 3);
    anim.SetFloat(YVel, rb.linearVelocity.y);
  }
  
  private void OnFlipped(bool i) {
    poof.GetComponent<ParticleSystemRenderer>().flip = new Vector3(0, i ? 1 : 0, 0);
    (!i ? splash1 : splash2).Emit(10);
  }
  private void OnLanded() {
    poof.transform.localScale = new Vector3(1, 1, 1);
    poof.Emit(1);
    poof.transform.localScale = new Vector3(-1, 1, 1);
    poof.Emit(1);
  }
  
  private void OnMoved(int i) {
    if (!player.grounded) return;
    poof.transform.localScale = new Vector3(i < 0 ? 1 : -1, 1, 1);
    poof.Emit(1);
  }

  private void OnJump(int i) {
    poof.transform.localScale = new Vector3(i < 0 ? 1 : -1, 1, 1);
    poof.Emit(1);
  }

  private void OnCollisionStay2D(Collision2D other) {
    if (other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Artifact")) {
      anim.SetBool(Pushing, Mathf.Abs(Vector2.Dot(other.contacts[0].point, new Vector2(PlayerController.dir, 0))) > 0.5f);
    }
  }
  private void OnCollisionExit2D(Collision2D other) {
    if(other.gameObject.CompareTag("Box") || other.gameObject.CompareTag("Artifact"))
      anim.SetBool(Pushing, false);
  }
}
