using UnityEngine;

public class Box : FlippableObject {
  [SerializeField] private bool spawnFlipped;

  protected override void Start() {
    base.Start();
    flipped = spawnFlipped;
  }
  
  private void OnCollisionStay2D(Collision2D other) {
    if(other.gameObject.CompareTag("Waterline")) canFlip = true;
  }
  private void OnCollisionExit2D(Collision2D other) {
    if(other.gameObject.CompareTag("Waterline")) canFlip = false;
  }
  private void FixedUpdate() => rb.linearVelocity = new Vector2(
    rb.linearVelocity.x * 0.9f,
    ApplyGravity(rb.linearVelocity.y));
}
