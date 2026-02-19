using UnityEngine;
public class SoundManager : MonoBehaviour {
  [SerializeField] private AudioSource[] sounds;
  
  public enum SoundType {
    DOOR_CLOSE, DOOR_OPEN, JUMP, LAND, 
    PICKUP, PRESSURE_PLATE, SPLASH, STEP, 
    UI_BACK, UI_HOVER, UI_SELECT
  }
  public delegate void PlaySound(SoundType type);
  public static event PlaySound OnSound;

  private void OnEnable() => OnSound += PlayASound;
  private void OnDisable() => OnSound += PlayASound;
  

  private void PlayASound(SoundType type) {
    
  }
}
