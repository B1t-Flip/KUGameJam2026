using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSounds : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler {
  public void OnPointerClick(PointerEventData eventData) {
    SoundManager.PlaySound(SoundManager.SoundType.UI_SELECT);
  }
  public void OnPointerEnter(PointerEventData eventData) {
    SoundManager.PlaySound(SoundManager.SoundType.UI_HOVER);
  }
}
