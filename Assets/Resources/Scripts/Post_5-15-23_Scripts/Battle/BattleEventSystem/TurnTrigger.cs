using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTrigger : MonoBehaviour
{
    public TriggerEvent[] triggerEvent;

    public event Action TriggerAction; 

    private bool turnChange = false;

    private void Start()
    {

        foreach (TriggerEvent trigger in triggerEvent)
        {
            TriggerAction += trigger.TriggerMe;
        }

    }

    private void Update()
    {
        if (!turnChange)
        {
            if (CombatSingleton.Instance.battleSystem.State == BATTLESTATE.TURNCHANGE)
            {
                turnChange = true;
                ActivateTriggers();
            }
        }
        else
        {
            if (CombatSingleton.Instance.battleSystem.State != BATTLESTATE.TURNCHANGE)
            {
                turnChange = false;
            }
        }
    }

    public void ActivateTriggers()
    {
        if(triggerEvent.Length > 0)
        {
            TriggerAction();
        }
    }
}
