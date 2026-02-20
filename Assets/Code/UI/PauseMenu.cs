using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour {
  [SerializeField] private InputActionReference pause;
  [SerializeField] private GameObject menu;
  [SerializeField] private SettingsManager setman;
  private bool paused;

  private void OnEnable() => pause.action.performed += OnPause;
  private void OnDisable() => pause.action.performed -= OnPause;
  

  private void OnPause(InputAction.CallbackContext ctx) {
    paused = !paused;
    Time.timeScale = paused ? 0 : 1;
    menu.SetActive(paused);
  }

  private void Start() {
    setman.OnEnable();
    Time.timeScale = 1;
    menu.SetActive(false);
    setman.gameObject.SetActive(false);
    
  }
}
