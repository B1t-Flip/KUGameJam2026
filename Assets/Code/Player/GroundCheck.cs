using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GroundCheck : MonoBehaviour {
  private PlayerController player;
  private int coyoteFrameCount, jumpFrameCount;
  private bool coyoteTime;
  [SerializeField] private List<string> groundTags;
  [SerializeField] private int coyoteFrames;
  
  [SerializeField] private InputActionReference flip;
  private FlippableObject flipTarget;
  private void Awake() => player = GetComponentInParent<PlayerController>();
  private void StartJumpFrameCount(int i) => jumpFrameCount = 5;

  private void OnEnable() {
    player.PlayerJumped += StartJumpFrameCount;
    flip.action.performed += FlipTarget;
  }

  private void OnDisable() {
    player.PlayerJumped -= StartJumpFrameCount;
    flip.action.performed -= FlipTarget;
  }

  private void FlipTarget(InputAction.CallbackContext ctx) {
    if (flipTarget && flipTarget.canFlip) 
      flipTarget.Flip(flipTarget.transform.localScale.y);
  }
  // you ever try to jump off a ledge, but you happened to go off of it just before you pressed jump, so you just fall to the floor?
  // coyote frames give the player a couple frames of leeway after walking off the edge so something like that doesn't happen.
  private void FixedUpdate() {
    if (jumpFrameCount > 0) jumpFrameCount--;
    if (!coyoteTime) return;
    if(coyoteFrameCount > 0) coyoteFrameCount--;
    else coyoteTime = player.grounded = false;
  }

  private void OnTriggerStay2D(Collider2D other){
    // use inverted ifs to reduce nesting
    player.canFlip = other.gameObject.CompareTag("Waterline");
    if (other.gameObject.CompareTag("Box") && !flipTarget) flipTarget = other.GetComponent<FlippableObject>();
    if (jumpFrameCount > 0 || !groundTags.Contains(other.gameObject.tag)) return;
    if(!player.grounded) player.Landed();
    player.grounded = true;
  }

  private void OnTriggerExit2D(Collider2D other) {
    if (other.gameObject.CompareTag("Box") && flipTarget) flipTarget = null;
    if (!groundTags.Contains(other.gameObject.tag) || !player.grounded) return;
    coyoteFrameCount = coyoteFrames;
    coyoteTime = true;
  }
}