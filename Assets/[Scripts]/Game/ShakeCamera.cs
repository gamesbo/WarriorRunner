using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShakeCamera : MonoBehaviour
{
    [HideInInspector] public Camera cam;

    private float shakeDuration = .5f;

    public Transform camTransform;

    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;


    #region Singleton
    public static ShakeCamera instance = null;
    private void Awake()
    {
        if (instance == null) { instance = this; }
        cam = GetComponent<Camera>();
    }
    #endregion

    void Update()
    {
        originalPos = camTransform.localPosition;
    }

    public IEnumerator ShakeCameraEnum()
    {
        shakeDuration = .5f;
        float duration = shakeDuration;
        while (shakeDuration > 0)
        {
            float factor = shakeDuration / duration;
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount * factor;
            shakeDuration -= Time.deltaTime * decreaseFactor;

            yield return null;
        }

        shakeDuration = 0f;
        camTransform.localPosition = originalPos;
    }
}
