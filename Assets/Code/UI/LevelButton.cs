using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {
  [SerializeField] private string title;
  [SerializeField] private int index;
  [SerializeField] private TMP_Text timeText, titleText;
  private Button button;

  private void Start() {
    button = GetComponent<Button>();
    titleText.text = title;
    if (SaveManager.currentSave.levelTimes[index - 1] == 0) timeText.text = "No Record Set";
    else {
      TimeSpan time = TimeSpan.FromSeconds(SaveManager.currentSave.levelTimes[index - 1]);
      timeText.text = "Record: " + time.ToString("mm':'ss'.'ff");
    }
    button.interactable = index <= SaveManager.currentSave.progression &&
                          index != SceneManager.GetActiveScene().buildIndex;
  }

  public void OnClicked() => SceneManager.LoadScene(index);
}
