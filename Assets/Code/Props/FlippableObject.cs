using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class FlippableObject : MonoBehaviour {
  [HideInInspector] public bool flipped;
  [SerializeField] protected float gravity;
  protected Rigidbody2D rb;
  protected Collider2D coll;
  
  public bool canFlip;

  protected bool flipOver;
  protected virtual void Start() {
    rb = GetComponent<Rigidbody2D>();
    coll = GetComponent<Collider2D>();
    flipOver = true;
  }

  protected float ApplyGravity(float velocity) {
    velocity += flipped ? gravity : -gravity;
    return velocity;
  }

  public void Flip(float height) {
    flipOver = false;
    transform.position = new Vector3(
      transform.position.x,
      transform.position.y + (-height * 3),
      transform.position.z);
    coll.isTrigger = true;
    flipped = !flipped;
    SoundManager.PlaySound(SoundManager.SoundType.SPLASH);
    StartCoroutine(FlipAnimation(5, -height));
  }

  private IEnumerator FlipAnimation(int frames, float height) {
    float startFrames = frames;
    for (; frames >= 0; --frames) {
      transform.localScale = new Vector3(1, height * 2 - height * (1 - frames / startFrames), 1);
      rb.linearVelocity = Vector2.zero;
      yield return new WaitForFixedUpdate();
    }
    coll.isTrigger = false;
    flipOver = true;
  }

}
