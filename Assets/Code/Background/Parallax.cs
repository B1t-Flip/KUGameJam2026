using System;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {
  private List<Transform> go;
  private Transform cam;

  private void Start() {
    cam = Camera.main?.transform;
    go = new List<Transform>();
    foreach (Transform child in transform) go.Add(child);
  }

  private void LateUpdate() {
    for (int i = 0; i < go.Count; i++) {
      float parallax = (float)i / go.Count;
      go[i].position = new Vector3(parallax * cam.position.x, go[i].position.y);
    }
    //if(SaveManager.currentSave.simplistic)
  }
}