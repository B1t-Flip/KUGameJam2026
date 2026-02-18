using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class FlippableObject : MonoBehaviour {
  [HideInInspector] public bool flipped;
  [SerializeField] protected float gravity;
  protected Rigidbody2D rb;
  protected Collider2D coll;

  protected bool flipOver;
  protected virtual void Start() {
    rb = GetComponent<Rigidbody2D>();
    coll = GetComponent<Collider2D>();
  }

  protected float ApplyGravity(float velocity) {
    velocity += flipped ? gravity : -gravity;
    return velocity;
  }

  public void Flip(float height) {
    flipped = !flipped;
    transform.position = new Vector3(
      transform.position.x,
      transform.position.y + (flipped ? height * 2 : -height * 2),
      transform.position.z);
    coll.isTrigger = true;
    transform.localScale = new Vector3(1, height * 2, 1);
    StartCoroutine(FlipAnimation(5, flipped ? height : -height));
  }

  private IEnumerator FlipAnimation(int frames, float height) {
    float startFrames = frames;
    for (; frames >= 0; --frames) {
      transform.localScale = new Vector3(1, height * 2 - height * (1 - frames / startFrames), 1);
      rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
      yield return new WaitForFixedUpdate();
    }
    coll.isTrigger = false;
    flipOver = true;
  }

}
