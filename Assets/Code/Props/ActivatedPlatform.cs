using System;
using UnityEngine;

public class ActivatedPlatform : MonoBehaviour {
    [SerializeField] private Vector2 pos1, pos2;
    private bool activated;
    private PlayerController player;
    private Rigidbody2D rb;
    
    [SerializeField] private float speed;
    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Activate() => activated = true;
    public void Deactivate() => activated = false;
    private void OnDrawGizmos() {
        Gizmos.DrawLine(pos1, pos2);
        Gizmos.DrawSphere(pos1, .25f);
        Gizmos.DrawSphere(pos2, .25f);
    }

    private void FixedUpdate() {
        if (!activated && ((Vector3)pos1 - transform.position).magnitude > .1f) {
            rb.linearVelocity = ((Vector3)pos1 - transform.position).normalized * speed;
        }
        else if (activated && ((Vector3)pos2 - transform.position).magnitude > .1f) {
            rb.linearVelocity = ((Vector3)pos2 - transform.position).normalized * speed;
        }
        else rb.linearVelocity = Vector3.zero;
    }
    
    private void OnCollisionStay2D(Collision2D other) {
        if (!other.gameObject.CompareTag("Player")) return;
        if (!player) player = other.gameObject.GetComponent<PlayerController>();
        if (!player.grounded) return;
        if (transform.InverseTransformPoint(other.contacts[0].point).y < 0.25f) return;
        player.transform.position += (Vector3)rb.linearVelocity * Time.deltaTime;
    }
}
