using System;
using UnityEngine;

public class LaserRedirector : Box {
  public Vector2Int laserReceived;
  private LineRenderer laserLine;
  private RaycastHit2D lastHit;

  private LaserRedirector redirector;
  private LaserReceiver receiver;

  private LayerMask layerMask;

  private SpriteRenderer sprite;
  
  [SerializeField] private bool directionFlipped;
  protected override void Start() {
    base.Start();
    laserLine = GetComponent<LineRenderer>();
    sprite = GetComponent<SpriteRenderer>();
    layerMask = LayerMask.GetMask("Default", "Laser");
  }
  private void FixedUpdate() {
    rb.linearVelocity = new Vector2(
      rb.linearVelocity.x * 0.9f,
      ApplyGravity(rb.linearVelocity.y));
    
    sprite.flipX = directionFlipped;
    if (laserReceived == Vector2Int.zero) {
      laserLine.enabled = false;
      if(redirector) redirector.laserReceived = Vector2Int.zero;
      if(receiver) receiver.OnLaserLost();
      return;
    }

    Vector2Int targetPoint = new(
      directionFlipped ? laserReceived.y : -laserReceived.y, 
      directionFlipped ? -laserReceived.x : laserReceived.x);
    targetPoint = new Vector2Int(
      flipped ? -targetPoint.x : targetPoint.x, 
      flipped ? -targetPoint.y : targetPoint.y);
    laserLine.enabled = true;
    RaycastHit2D hit = Physics2D.Raycast(
      transform.position, 
      targetPoint, 
      1000, 
      layerMask);
    if (!hit) {
      laserLine.SetPositions(new []{transform.position, transform.position + (Vector3)(Vector2)targetPoint * 1000});
      if(redirector) redirector.laserReceived = Vector2Int.zero;
      if(receiver) receiver.OnLaserLost();
      lastHit = new RaycastHit2D();
      return;
    }

    Vector3 target = hit.point;
    
    if (!lastHit) lastHit = hit;
    else if (lastHit.collider.gameObject == hit.collider.gameObject) {
      if (hit.collider.CompareTag("Redirector") || hit.collider.CompareTag("Receiver"))
        target = hit.collider.transform.position;
      laserLine.SetPositions(new []{transform.position, target});
      return;
    }
    
    GameObject obj = hit.collider.gameObject;
    Redirect(obj, ref target, targetPoint);
    lastHit = hit;
    laserLine.SetPositions(new []{transform.position, target});
  }

  private void Redirect(GameObject obj, ref Vector3 redirect, Vector2Int dir) {
    switch (obj.tag) {
      case "Redirector":
        redirector = obj.GetComponent<LaserRedirector>();
        redirector.laserReceived = new Vector2Int(
          dir.x, 
          dir.y);
        redirect = redirector.transform.position;
        if(receiver) receiver.OnLaserLost();
        break;
      case "Receiver":
        receiver = obj.GetComponent<LaserReceiver>();
        laserLine.SetPositions(new []{transform.position, receiver.transform.position});
        receiver.OnLaserReceived();
        redirect = receiver.transform.position;
        if(redirector) redirector.laserReceived = Vector2Int.zero;
        break;
      default:
        if(receiver) receiver.OnLaserLost();
        if(redirector) redirector.laserReceived = Vector2Int.zero;
        break;
    }
  }
  
}
