using System;
using UnityEngine;

public class Pedestal : MonoBehaviour {
  [HideInInspector] public bool artifactPlaced;
  [SerializeField] private SpriteRenderer fauxArtifact;

  private void OnTriggerEnter2D(Collider2D other) {
    if (!other.gameObject.CompareTag("Artifact")) return;
    artifactPlaced = true;
    other.gameObject.SetActive(false);
    fauxArtifact.gameObject.SetActive(true);
  }
}
