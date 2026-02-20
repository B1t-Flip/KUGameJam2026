using UnityEngine;
using UnityEngine.EventSystems;

public class BackButtonSounds : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler {
  public void OnPointerClick(PointerEventData eventData) {
    SoundManager.PlaySound(SoundManager.SoundType.UI_BACK);
  }
  public void OnPointerEnter(PointerEventData eventData) {
    SoundManager.PlaySound(SoundManager.SoundType.UI_HOVER);
  }
}
