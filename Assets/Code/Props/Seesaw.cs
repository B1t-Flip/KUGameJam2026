using System;
using UnityEngine;

public class Seesaw : MonoBehaviour {
  [SerializeField] private Seesaw partner;
  
  [SerializeField] private float speed, maxShift;
  private PlayerController player;
  private Rigidbody2D rb;

  private Vector3 startPos, partnerStartPos;
  [HideInInspector] public bool tampered, currentlyTouching;

  private void Start() {
    rb = GetComponent<Rigidbody2D>();
    partnerStartPos = partner.transform.position;
    startPos = transform.position;
  }

  private void FixedUpdate() {
    if (Vector3.Distance(transform.position, startPos) < 0.1f || partner.currentlyTouching) tampered = false;
    
    if (tampered) partner.transform.position = 
      new Vector3(partnerStartPos.x, partnerStartPos.y - (transform.position.y - startPos.y));

    if (transform.position.y - startPos.y > maxShift) 
      transform.position = new Vector3(transform.position.x, startPos.y + maxShift);
    else if (transform.position.y - startPos.y < -maxShift) 
      transform.position = new Vector3(transform.position.x, startPos.y - maxShift);
    rb.linearVelocity *= 0.9f;
  }

  private void OnCollisionStay2D(Collision2D other) {
    if (!other.gameObject.CompareTag("Player")) return;
    if (!player) player = other.gameObject.GetComponent<PlayerController>();
    if (!player.grounded) return;
    if (transform.InverseTransformPoint(other.contacts[0].point).y < 0.25f) return;
    tampered = true;
    currentlyTouching = true;
    if(player.flipped) rb.linearVelocity -= Vector2.down * speed * Time.deltaTime;
    else rb.linearVelocity += Vector2.down * speed * Time.deltaTime;
    //player.transform.position += (Vector3)rb.linearVelocity * Time.deltaTime;
  }

  private void OnCollisionExit2D(Collision2D other) {
    if (!other.gameObject.CompareTag("Player")) return;
    currentlyTouching = false;
  }
}
