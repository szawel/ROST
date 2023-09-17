using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The RostDinn class provides visual effects for rendering camera images. It applies a dithering
/// material followed by a threshold material to process the image, creating a unique visual style.
/// </summary>
public class RostDinn : MonoBehaviour
{
    public Material ditherMat;     // Material for dithering effect.
    public Material thresholdMat;  // Material for threshold effect.
    public Camera cam;             // Reference to the Camera component.

    /// <summary>
    /// Initialization method. Fetches the camera component.
    /// </summary>
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update method remains empty for now.
    void Update() { }

    /// <summary>
    /// Called after all rendering is complete to render image. Applies the dithering
    /// and threshold effects using the defined materials.
    /// </summary>
    /// <param name="src">The source render texture.</param>
    /// <param name="dst">The destination render texture.</param>
    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        // Temporary render textures for image processing.
        RenderTexture large = RenderTexture.GetTemporary(1640, 940, 0, RenderTextureFormat.ARGB32);
        RenderTexture main = RenderTexture.GetTemporary(820, 470, 0, RenderTextureFormat.ARGB32);

        large.filterMode = FilterMode.Bilinear;
        main.filterMode = FilterMode.Bilinear;

        // Calculate camera frustum corners for dithering.
        Vector3[] corners = new Vector3[4];
        cam.CalculateFrustumCorners(new Rect(0, 0, 1, 1), cam.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, corners);

        for (int i = 0; i < 4; i++)
        {
            corners[i] = transform.TransformVector(corners[i]);
            corners[i].Normalize();
        }

        // Set dithering material properties.
        ditherMat.SetVector("_BL", corners[0]);
        ditherMat.SetVector("_TL", corners[1]);
        ditherMat.SetVector("_TR", corners[2]);
        ditherMat.SetVector("_BR", corners[3]);

        // Apply dithering and thresholding effects.
        Graphics.Blit(src, large, ditherMat);
        Graphics.Blit(large, main, thresholdMat);
        Graphics.Blit(main, dst);

        // Release the temporary render textures.
        RenderTexture.ReleaseTemporary(large);
        RenderTexture.ReleaseTemporary(main);
    }
}
