using System;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour {

  [SerializeField] private UnityEvent OnPressed;
  [SerializeField] private UnityEvent OnUnPressed;
  private Vector3 startPos;

  private bool pressed;

  private void Start() => startPos = transform.position;

  private void OnTriggerStay2D(Collider2D other) {
    if (pressed) return;
    if ((!other.gameObject.CompareTag("Player") && 
         !other.gameObject.CompareTag("Box") && 
         !other.gameObject.CompareTag("Artifact"))) return;
    transform.position = startPos + Vector3.down * 0.0645f;
    OnPressed.Invoke();
    SoundManager.PlaySound(SoundManager.SoundType.PRESSURE_PLATE);
    pressed = true;
  }
  private void OnTriggerExit2D(Collider2D other) {
    if (!other.gameObject.CompareTag("Player") && 
        !other.gameObject.CompareTag("Box") &&
        !other.gameObject.CompareTag("Artifact")) return;
    transform.position = startPos;
    OnUnPressed.Invoke();
    SoundManager.PlaySound(SoundManager.SoundType.PRESSURE_PLATE);
    pressed = false;
  }
}
