using TMPro;
using UnityEngine;

public class WinMenu : MonoBehaviour {
  [SerializeField] private GameObject winMenu;
  [SerializeField] private TMP_Text text;

  public void OnWin() {
    winMenu.SetActive(true);
    text.text = $"You Win!\nTime: {LevelTimer.inverseTimer}";
    Time.timeScale = 0;
  }
}
