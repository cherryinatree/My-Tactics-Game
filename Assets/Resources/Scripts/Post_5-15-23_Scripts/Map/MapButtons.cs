using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButtons : MonoBehaviour
{

    public PointMaster point;

    public void Accept()
    {
        point.Accept();
    }

    public void Previous()
    {
        point.Previous();
    }

    public void Next()
    {
        point.Next();
    }
}
