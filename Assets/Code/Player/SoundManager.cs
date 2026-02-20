using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour {
  private static AudioSource[] sounds;
  
  public enum SoundType {
    DOOR_CLOSE, DOOR_OPEN, JUMP, LAND, 
    PICKUP, PRESSURE_PLATE, SPLASH, STEP, 
    UI_BACK, UI_HOVER, UI_SELECT, PUSH
  }

  private void Start() => sounds = GetComponentsInChildren<AudioSource>();

  public static void PlaySound(SoundType type) {
    sounds[(int)type].Play();
    sounds[(int)type].loop = false;
  }
  public static void PlaySoundPitched(SoundType type, float range) {
    sounds[(int)type].pitch = 1 + Random.Range(-range, +range);
    sounds[(int)type].loop = false;
    sounds[(int)type].Play();
  }

  public static void LoopSound(SoundType type) {
    if(!sounds[(int)type].isPlaying) sounds[(int)type].Play();
    sounds[(int)type].loop = true;
  } 
  public static void StopLoop(SoundType type) {
    sounds[(int)type].Stop();
    sounds[(int)type].loop = false;
  } 
  
}
