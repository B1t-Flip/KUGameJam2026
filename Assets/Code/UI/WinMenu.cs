using System;
using TMPro;
using UnityEngine;

public class WinMenu : MonoBehaviour {
  [SerializeField] private GameObject winMenu;
  [SerializeField] private TMP_Text text;

  public void OnWin(float time) {
    winMenu.SetActive(true);
    
    TimeSpan t = TimeSpan.FromSeconds(time);
    text.text = "You Win!\nTime: " + t.ToString("mm':'ss'.'ff");
    Time.timeScale = 0;
  }
}
