using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Settings
{
    private float _mouseSensativity = 100f;


    public float MouseSensativity
    {
        get { return _mouseSensativity; }
        set { value = _mouseSensativity; }
    }
}
