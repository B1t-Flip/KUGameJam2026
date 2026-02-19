using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickupController : MonoBehaviour {
  [SerializeField] private InputActionReference pickupBinding;
  
  [CanBeNull] private GameObject currentTargetPickup;
  private GameObject storedPickup;
  private int dir;

  private BoxCollider2D coll;
  [SerializeField] private SpriteRenderer pickupDisplay;

  public static bool carryingSomething;

  private void Start() {
    coll = GetComponent<BoxCollider2D>();
    pickupDisplay.gameObject.SetActive(false);
  }

  private void OnEnable() {
    GroundCheck.StandingOnPickup += OnStandingOnPickup;
    pickupBinding.action.performed += OnPickup;
  }

  private void OnDisable() {
    GroundCheck.StandingOnPickup -= OnStandingOnPickup;
    pickupBinding.action.performed -= OnPickup;
  }

  private void OnStandingOnPickup(GameObject pickup) {
    if (storedPickup) return;
    currentTargetPickup = pickup;
  }

  private void FixedUpdate() {
    if (PlayerController.dir != 0) dir = PlayerController.dir;
    Debug.DrawRay(transform.position + new Vector3(0, -coll.size.y / 4), new Vector3(dir, 0), Color.red);
  }

  private void OnPickup(InputAction.CallbackContext ctx) {
    
    if (storedPickup) {
      RaycastHit2D hit = Physics2D.Raycast(
        transform.position + new Vector3(0, -coll.size.y / 4),
        new Vector2(dir, 0), 1f,
        LayerMask.GetMask("Default"));
      if (hit) return;
      
      coll.size -= new Vector2(0, .5f);
      coll.offset -= new Vector2(0, .25f);
      
      storedPickup.transform.position = 
        transform.position + 
        new Vector3(dir * 1.1f, -coll.size.y / 4);
      
      storedPickup.SetActive(true);
      pickupDisplay.gameObject.SetActive(false);
      storedPickup = null;
      carryingSomething = false;
    }
    else if (currentTargetPickup) {
      storedPickup = currentTargetPickup;
      currentTargetPickup = null;
      
      coll.size += new Vector2(0, .5f);
      coll.offset += new Vector2(0, .25f);
      
      storedPickup.SetActive(false);
      pickupDisplay.gameObject.SetActive(true);
      pickupDisplay.sprite = storedPickup.GetComponent<SpriteRenderer>().sprite;
      carryingSomething = true;
    }
  }
}
