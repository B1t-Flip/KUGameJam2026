using UnityEngine;

public class QuitGame : MonoBehaviour {
  private void Quit() {
    Application.Quit();
    Debug.Log("Game is exiting");
    //Just to make sure its working
  }
}