using System;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour {

  [SerializeField] private UnityEvent OnPressed;
  [SerializeField] private UnityEvent OnUnPressed;
  private Vector3 startPos;

  private void Start() => startPos = transform.position;

  private void OnTriggerEnter2D(Collider2D other) {
    if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Box")) return;
    transform.position = startPos + Vector3.down * 0.0645f;
    OnPressed.Invoke();
  }
  private void OnTriggerExit2D(Collider2D other) {
    if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Box")) return;
    transform.position = startPos;
    OnUnPressed.Invoke();
  }
}
