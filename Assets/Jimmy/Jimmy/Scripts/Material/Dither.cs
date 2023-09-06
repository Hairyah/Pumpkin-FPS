using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dither : MonoBehaviour
{
    public Material ditherMat;
    public Material thresholdMat;
    public Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        RenderTexture large = RenderTexture.GetTemporary(1640, 940, 0, RenderTextureFormat.ARGB32);
        RenderTexture main = RenderTexture.GetTemporary(820, 470, 0, RenderTextureFormat.ARGB32);
        large.filterMode = FilterMode.Bilinear;
        main.filterMode = FilterMode.Bilinear;

        Vector3[] corners = new Vector3[4];

        //cam.CalculateFrustumCorners(new Rect(0, 0, 1, 1), cam.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, corners);

        for (int i = 0; i < 4; i++)
        {
            corners[i] = transform.TransformVector(corners[i]);
            corners[i].Normalize();
        }

        ditherMat.SetVector("_BL", corners[0]);
        ditherMat.SetVector("_TL", corners[1]);
        ditherMat.SetVector("_TR", corners[2]);
        ditherMat.SetVector("_BR", corners[3]);

        Graphics.Blit(source, large, ditherMat);
        Graphics.Blit(large, main, thresholdMat);
        Graphics.Blit(main, destination);

        // Graphics.Blit(source, main, ditherMat);
        // Graphics.Blit(main, destination);

        RenderTexture.ReleaseTemporary(large);
        RenderTexture.ReleaseTemporary(main);
    }
}
