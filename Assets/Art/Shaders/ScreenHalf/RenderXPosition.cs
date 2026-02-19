using UnityEngine;
using UnityEngine.UI;
// Setting the camera's x position in the shader so the water is animated correctly when moving
public class RenderXPosition : MonoBehaviour {
  private static readonly int XPos = Shader.PropertyToID("_XPos");
  private RawImage image;
  private Material renderMat;

  private Transform renderCam;

  private void Start() {
    image = GetComponent<RawImage>();
    image.material = new Material(image.material);
    renderMat = image.material;
    renderCam = Camera.main?.transform;
  }

  private void FixedUpdate() {
    renderMat.SetFloat(XPos, renderCam.position.x);
  }
}
