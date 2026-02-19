using System.Collections.Generic;
using UnityEngine;

public class SeesawLine : MonoBehaviour {
  private LineRenderer line;
  [SerializeField] private Transform[] targets;

  private void Start() => line = GetComponent<LineRenderer>();

  private void FixedUpdate() {
    List<Vector3> positions = new();
    foreach (Transform target in targets) {
      positions.Add(target.position);
      positions.Add(transform.position);
    }
    line.SetPositions(positions.ToArray());
  }
}
