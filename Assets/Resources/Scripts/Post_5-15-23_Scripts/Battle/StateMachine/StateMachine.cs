using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public IState currentState;
    public StatePatrol statePatrol = new StatePatrol();
    private bool isAI;

    // Set the initial state
    private void Start()
    {
        if(GetComponent<CombatCharacter>().team != 0)
        {
            isAI = true;
            TransitionToState(statePatrol);
        }
        else
        {
            isAI = false;
        }
    }

    // Update the state machine
    private void Update()
    {
        if (ConditionsToActivate())
        {
            currentState.UpdateState(this);
        }
       
    }

    // Transition to a new state
    public void TransitionToState(IState newState)
    {

        currentState = newState;
        currentState.EnterState(this);
    }


    private bool ConditionsToActivate()
    {
        if ( 

            isAI && 
            gameObject == CombatSingleton.Instance.FocusCharacter &&
            CombatSingleton.Instance.battleSystem.State == BATTLESTATE.ENEMYTURN &&
            gameObject.GetComponent<CombatCharacter>().myStats.actionsRemaining > 0

            )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
