using System.Linq;
using UnityEngine;

public class WinPortal : MonoBehaviour {
  private Pedestal[] pedestals;
  private ParticleSystem.EmissionModule emission;

  private void Start() {
    pedestals = FindObjectsByType<Pedestal>(FindObjectsSortMode.None);
    emission = GetComponent<ParticleSystem>().emission;
  }

  private void FixedUpdate() => emission.rateOverTime = pedestals.All(x => x.artifactPlaced) ? 40 : 0;
}
