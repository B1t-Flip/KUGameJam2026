using UnityEngine;

public class PlayerXFollow : MonoBehaviour {
  [SerializeField] private Transform player;
  [SerializeField] private float followSpeed;
  [SerializeField] private int pixelScale;

  private void FixedUpdate() {
    Vector3 targetPos = new Vector3(
      Mathf.Lerp(transform.position.x, player.position.x, followSpeed), 
      transform.position.y, -20);
    ToNearest(ref targetPos.x, 1f / pixelScale);
    transform.position = targetPos;
  }

  private static void ToNearest(ref float value, float increment) {
    int count = Mathf.CeilToInt(value / increment);
    value = count * increment;
  }
}
