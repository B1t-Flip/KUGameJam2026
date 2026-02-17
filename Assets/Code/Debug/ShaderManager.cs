using UnityEngine;
using UnityEngine.Rendering.Universal;

[ExecuteAlways]
public class ShaderManager : MonoBehaviour {
  [SerializeField] private Renderer2DData render;

  private void Update() {
    render.rendererFeatures[0].SetActive(Application.isPlaying);
  }
}