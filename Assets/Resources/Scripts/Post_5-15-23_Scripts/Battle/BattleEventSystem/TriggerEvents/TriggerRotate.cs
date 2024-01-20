using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRotate : TriggerEvent
{

    public override void TriggerMe()
    {
        transform.Rotate(90, 0, 0);
    }
}
