using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour {
  //[SerializeField] private PlayerController player;
  [SerializeField] private float levelTimer;
  private bool timerStarted;

  [SerializeField] private Slider[] sliders;
  [SerializeField] private TMP_Text text;
  
  public static float timer, inverseTimer;

  public static event PlayerController.Notify TimerFailed;
  private bool invoked;
  
  private void Start() {
    timerStarted = false;
    timer = levelTimer;
    foreach (Slider slider in sliders) slider.maxValue = levelTimer;
  }

  private void OnEnable() => PlayerController.PlayerMoved += OnPlayerMoved;
  private void OnDisable() => PlayerController.PlayerMoved -= OnPlayerMoved;
  

  private void OnPlayerMoved(int i) {
    if (i != 0) timerStarted = true;
  }

  private void Update() {
    foreach (Slider slider in sliders) slider.value = timer;
    
    TimeSpan time = TimeSpan.FromSeconds(timer);
    text.text = time.ToString(timer < 0 ? "'-'mm':'ss'.'ff" : "mm':'ss'.'ff");

    if (!timerStarted) return;
    timer -= Time.deltaTime;
    inverseTimer = -timer + levelTimer;

    if (timer > 0 || invoked) return;
    TimerFailed?.Invoke();
    invoked = true;
  }
}
