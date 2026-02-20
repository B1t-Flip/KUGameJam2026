using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering.Universal;


[Serializable]
public enum ColorPalette { NORMAL, PROTANOPIA, DEUTERANOPIA, TRITANOPIA, GRAYSCALE }
[Serializable]
public class SaveData {
  public int progression = 1;
  public float[] levelTimes = { 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f };

  public bool simplistic = false;
  public ColorPalette palette = ColorPalette.NORMAL;
  public float masterVolume = 1, sfxVolume = 1, musicVolume = 1;
}

public class SaveManager : MonoBehaviour {
  public static SaveData currentSave = new();

  private void Awake() { if (!Load(ref currentSave)) Save(currentSave); } 
  public static bool Load(ref SaveData saveData) {
    string fullPath = Path.Combine(Application.persistentDataPath, "save.json");
    string result;
    try { result = File.ReadAllText(fullPath); }
    catch (Exception e) { return false; }
    saveData = JsonUtility.FromJson<SaveData>(result);
    return true;
  }
  
  public static bool Save(SaveData saveData) {
    string fullPath = Path.Combine(Application.persistentDataPath, "save.json");
    string content = JsonUtility.ToJson(saveData);
    try {
      File.WriteAllText(fullPath, content);
      return true;
    }
    catch (Exception e) { return false; }
  }
}
