using System;
using UnityEngine;

public class SimpleCheck : MonoBehaviour {
  private Parallax parallax;
  private void Start() => parallax = GetComponentInChildren<Parallax>();
  private void Update() => parallax.gameObject.SetActive(!SaveManager.currentSave.simplistic);
  
}
