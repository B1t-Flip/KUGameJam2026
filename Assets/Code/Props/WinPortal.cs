using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPortal : MonoBehaviour {
  private Pedestal[] pedestals;
  private ParticleSystem.EmissionModule emission;
  private bool winCon;

  private void Start() {
    pedestals = FindObjectsByType<Pedestal>(FindObjectsSortMode.None);
    emission = GetComponent<ParticleSystem>().emission;
  }

  private void FixedUpdate() {
    winCon = pedestals.All(x => x.artifactPlaced) && LevelTimer.timer > 0;
    emission.rateOverTime = winCon ? 40 : 0;
  }

  private void OnTriggerEnter2D(Collider2D other) {
    if (!other.CompareTag("Player")) return;
    if (!winCon) return;
    
    FindFirstObjectByType<WinMenu>().OnWin(LevelTimer.inverseTimer);
    // you win
    int index = SceneManager.GetActiveScene().buildIndex;
    if(SaveManager.currentSave.levelTimes[index - 1] == 0 ||
       LevelTimer.inverseTimer < SaveManager.currentSave.levelTimes[index - 1])
      SaveManager.currentSave.levelTimes[index - 1] = LevelTimer.inverseTimer;
    
    if (SaveManager.currentSave.progression == index) SaveManager.currentSave.progression++;
    SaveManager.Save(SaveManager.currentSave);
  }
}
