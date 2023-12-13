using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalerTween : MonoBehaviour
{
    public Vector3 Size;
    public float Duration;

    private void OnEnable()
    {
        LeanTween.scale(gameObject, Size, Duration);

    }

    private void OnDisable()
    {

        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.5f);
    }
}
