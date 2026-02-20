using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData {
  public int progression;
  public List<float> levelTimes;

  public bool simplistic;
  public int palette;
  public float masterVolume, sfxVolume, musicVolume;
}

public class SaveManager : MonoBehaviour {
  private SaveData currentSave = new();
  [SerializeField] private Material normalPalette, protanopia, deuteranopia, tritanopia, grayscale;
}
