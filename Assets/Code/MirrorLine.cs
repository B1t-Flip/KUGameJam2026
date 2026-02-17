using UnityEngine;

public class MirrorLine : MonoBehaviour {
  private void OnDrawGizmos() {
    Gizmos.color = Color.green;
    Gizmos.DrawLine(new Vector2(-100, 0), new Vector2(100, 0));
  }
}
