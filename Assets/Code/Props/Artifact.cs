using UnityEngine;

public class Artifact : FlippableObject {
  [SerializeField] private bool spawnFlipped;

  protected override void Start() {
    base.Start();
    flipped = spawnFlipped;
    canFlip = false;
  }

  private void FixedUpdate() => rb.linearVelocity = new Vector2(
    rb.linearVelocity.x * 0.9f,
    ApplyGravity(rb.linearVelocity.y));
}
