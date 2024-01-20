using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTrigger : TriggerEvent
{
    private static bool isFirst = true;

    public bool isSingleUse = false;

    public TriggerEvent[] triggerEvent;

    public event Action TriggerAction;

    private void Awake()
    {
        type = TriggerType.triggers;
    }

    private void Start()
    {
        triggerData = new TriggerData();
        triggerData.isUsed = false;

        foreach(TriggerEvent trigger in triggerEvent)
        {
            TriggerAction += trigger.TriggerMe;
        }

        SetUpData();
    }

    public bool IsUsed()
    {
        return triggerData.isUsed;
    }

    public void ActivateTriggers()
    {
        if (!triggerData.isUsed)
        {
            TriggerAction();
        }

        if (isSingleUse)
        {
            triggerData.isUsed = true;
        }
    }


    public override void TriggerMe()
    {
        throw new NotImplementedException();
    }


}
