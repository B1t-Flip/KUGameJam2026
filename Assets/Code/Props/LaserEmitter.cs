using System;
using UnityEngine;

public class LaserEmitter : MonoBehaviour {
  private LineRenderer laserLine;
  private RaycastHit2D lastHit;
  private LaserRedirector redirector;
  private LaserReceiver receiver;

  [SerializeField] private bool active;

  public void Activate() => active = true;
  public void Deactivate() => active = false;

  private LayerMask layerMask;
  [SerializeField] private Vector2Int direction;

  private void Start() {
    laserLine = GetComponent<LineRenderer>();
    layerMask = LayerMask.GetMask("Default", "Laser");
  }

  private void FixedUpdate() {
    RaycastHit2D hit = Physics2D.Raycast(
      transform.position, 
      direction, 
      1000, 
      layerMask);
    if (!active) {
      if(receiver) receiver.OnLaserLost();
      if(redirector) redirector.laserReceived = Vector2Int.zero;
      receiver = null;
      redirector = null;
      laserLine.enabled = false;
      lastHit = new RaycastHit2D();
      return;
    }

    laserLine.enabled = true;
    if (!hit) {
      laserLine.SetPositions(new []{transform.position, transform.position + (Vector3)(Vector2)direction * 1000});
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
    Redirect(obj, ref target);
    lastHit = hit;
    laserLine.SetPositions(new []{transform.position, target});
  }

  private void Redirect(GameObject obj, ref Vector3 redirect) {
    switch (obj.tag) {
      case "Redirector":
        redirector = obj.GetComponent<LaserRedirector>();
        redirector.laserReceived = direction;
        redirect = redirector.transform.position;
        if(receiver) receiver.OnLaserLost();
        break;
      case "Receiver":
        receiver = obj.GetComponent<LaserReceiver>();
        laserLine.SetPositions(new[] { transform.position, receiver.transform.position });
        receiver.OnLaserReceived();
        redirect = receiver.transform.position;
        if(redirector) redirector.laserReceived = Vector2Int.zero;
        break;
    }
  }
}
