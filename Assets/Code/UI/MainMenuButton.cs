using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour {
  public void OnClick() {
    Time.timeScale = 1;
    SceneManager.LoadScene(0);
  }
}
