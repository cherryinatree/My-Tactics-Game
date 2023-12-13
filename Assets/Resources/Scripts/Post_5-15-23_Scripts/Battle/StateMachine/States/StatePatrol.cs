using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePatrol : IState
{

    GameObject target;

    public override void EnterState(StateMachine stateMachine)
    {
        target = null;
    }

    public override void UpdateState(StateMachine stateMachine)
    {
        FindPlayer(stateMachine);
    }

    public void StateCondtion()
    {

    }

    private void FindPlayer(StateMachine stateMachine)
    {
        StateTargeting stateTargeting = new StateTargeting();
        target = stateTargeting.FindTarget(stateMachine);
        DecideNextState(stateMachine);
    }

    private void DecideNextState(StateMachine stateMachine)
    {
        if(Vector3.Distance(stateMachine.transform.position, target.transform.position) > 2)
        {
            stateMachine.TransitionToState(new StateMove());
        }
        else
        {
            stateMachine.TransitionToState(new StateAttack());
        }
    }

}
