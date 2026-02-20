using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour {
  private static AudioSource[] sounds;
  private static List<float> volumes = new();
  public enum SoundType {
    DOOR_CLOSE, DOOR_OPEN, JUMP, LAND, 
    PICKUP, PRESSURE_PLATE, SPLASH, STEP, 
    UI_BACK, UI_HOVER, UI_SELECT, PUSH
  }


  private void Start() {
    sounds = GetComponentsInChildren<AudioSource>();
    foreach (AudioSource sound in sounds) volumes.Add(sound.volume);
  }

  private void Update() {
    sounds[^1].volume = volumes[^1] * SaveManager.currentSave.masterVolume * SaveManager.currentSave.musicVolume;
  }

  public static void PlaySound(SoundType type) {
    sounds[(int)type].volume = volumes[(int)type] * SaveManager.currentSave.masterVolume * SaveManager.currentSave.sfxVolume;
    sounds[(int)type].Play();
    sounds[(int)type].loop = false;
  }
  public static void PlaySoundPitched(SoundType type, float range) {
    sounds[(int)type].volume = volumes[(int)type] * SaveManager.currentSave.masterVolume * SaveManager.currentSave.sfxVolume;
    sounds[(int)type].pitch = 1 + Random.Range(-range, +range);
    sounds[(int)type].loop = false;
    sounds[(int)type].Play();
  }

  public static void LoopSound(SoundType type) {
    if (!sounds[(int)type].isPlaying) {
      sounds[(int)type].volume = volumes[(int)type] * SaveManager.currentSave.masterVolume * SaveManager.currentSave.sfxVolume;
      sounds[(int)type].Play();
    }
    sounds[(int)type].loop = true;
  } 
  public static void StopLoop(SoundType type) {
    sounds[(int)type].Stop();
    sounds[(int)type].loop = false;
  }

  
}
