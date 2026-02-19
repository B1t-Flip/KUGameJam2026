using UnityEngine;

public class Artifact : FlippableObject {
  [SerializeField] private bool flip;

  protected override void Start() {
    base.Start();
    flipped = flip;
    canFlip = false;
  }

  private void FixedUpdate() => rb.linearVelocity = new Vector2(
    rb.linearVelocity.x * 0.9f,
    ApplyGravity(rb.linearVelocity.y));
}
