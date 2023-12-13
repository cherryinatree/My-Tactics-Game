using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IState
{
    public abstract void EnterState(StateMachine stateMachine);
    public abstract void UpdateState(StateMachine stateMachine);
}
