using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnTriggerEveryOther : MonoBehaviour
{
    public TriggerEvent[] triggerEvent;

    public event Action TriggerAction;

    private bool turnChange = false;

    public bool isActionOnPlayerTurn = false;

    private BATTLESTATE state = BATTLESTATE.PLAYERTURN;

    private void Start()
    {
        if (isActionOnPlayerTurn)
        {
            state = BATTLESTATE.ENEMYTURN;
        }
        else
        {
            state = BATTLESTATE.PLAYERTURN; 
        }
        foreach (TriggerEvent trigger in triggerEvent)
        {
            TriggerAction += trigger.TriggerMe;
        }

    }

    private void Update()
    {

        if (!turnChange)
        {
            if(CombatSingleton.Instance.battleSystem.State == BATTLESTATE.TURNCHANGE)
            {
                turnChange = true;
                ActivateTriggers();
            }
        }


        if (CombatSingleton.Instance.battleSystem.State == state)
        {
            turnChange = false;
        }
        else
        {
            turnChange = true;
        }


        /*
        if (!turnChange)
        {
            if (CombatSingleton.Instance.battleSystem.State == state)
            {

                turnChange = true;
                ActivateTriggers();
            }
        }
        else
        {
            if (CombatSingleton.Instance.battleSystem.State != state && 
                CombatSingleton.Instance.battleSystem.State != BATTLESTATE.ACTION &&
                CombatSingleton.Instance.battleSystem.State != BATTLESTATE.TURNCHANGE)
            {
                turnChange = false;
            }
        }*/
    }

    public void ActivateTriggers()
    {
        if (triggerEvent.Length > 0)
        {
            TriggerAction();
        }
    }
}
