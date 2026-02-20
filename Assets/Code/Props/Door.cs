using System;
using UnityEngine;

public class Door : MonoBehaviour {
  private static readonly int Open = Animator.StringToHash("Open");
  private BoxCollider2D coll;
  private Animator anim;

  private void Start() {
    coll = GetComponent<BoxCollider2D>();
    anim = GetComponent<Animator>();
  }

  public void OpenDoor() {
    coll.enabled = false;
    anim.SetBool(Open, true);
    SoundManager.PlaySound(SoundManager.SoundType.DOOR_OPEN);
  }

  public void CloseDoor() {
    coll.enabled = true;
    anim.SetBool(Open, false);
    SoundManager.PlaySound(SoundManager.SoundType.DOOR_CLOSE);
  }
}
