using System;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
  [SerializeField] private Vector2[] positions;
  [SerializeField] private float speed;
  private int i;
  private PlayerController player;
  private Rigidbody2D rb;

  private void Start() {
    rb = GetComponent<Rigidbody2D>();
  }

  private void FixedUpdate() {
    rb.linearVelocity = ((Vector3)positions[i] - transform.position).normalized * speed;
    if (Vector2.Distance(transform.position, positions[i]) < .1f) i++;
    if (i >= positions.Length) i = 0;
  }

  private void OnDrawGizmos() {
    Vector2 lastPos = Vector2.zero;
    foreach (Vector2 position in positions) {
      if(lastPos.magnitude > 0.1f) Gizmos.DrawLine(lastPos, position);
      Gizmos.DrawSphere(position, .25f);
      lastPos = position;
    }
  }

  private void OnCollisionStay2D(Collision2D other) {
    if (!other.gameObject.CompareTag("Player")) return;
    if (!player) player = other.gameObject.GetComponent<PlayerController>();
    if (!player.grounded) return;
    if (transform.InverseTransformPoint(other.contacts[0].point).y < 0.25f) return;
    player.transform.position += (Vector3)rb.linearVelocity * Time.deltaTime;
  }
}
