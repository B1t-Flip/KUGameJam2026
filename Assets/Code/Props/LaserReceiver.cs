using UnityEngine;
using UnityEngine.Events;

public class LaserReceiver : MonoBehaviour {
  [SerializeField] private UnityEvent LaserReceived, LaserLost;

  public void OnLaserReceived() => LaserReceived.Invoke();
  public void OnLaserLost() => LaserLost.Invoke();
}
