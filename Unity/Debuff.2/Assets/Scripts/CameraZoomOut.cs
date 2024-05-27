using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraZoomOut : MonoBehaviour
{
    public Camera cameraToZoom;
    private float startSize = 0.2f;
    private float endSize = 5f;
    private float duration = 3f;

    // Start is called before the first frame update
    void Start()
    {
        if (cameraToZoom == null)
        {
            cameraToZoom = Camera.main;
        }
        StartCoroutine(ZoomOut());
    }

    IEnumerator ZoomOut()
    {
        float elapsed = 0f;
        float currentSize = startSize;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            t = t * t; // 가속하는 보간 함수 (exponential easing)
            currentSize = Mathf.Lerp(startSize, endSize, t);
            cameraToZoom.orthographicSize = currentSize;
            yield return null;
        }

        cameraToZoom.orthographicSize = endSize; // Ensure it reaches the end size
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
