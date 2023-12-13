using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : IState
{
    GameObject target;
    private Actions actions;
    private GameObject destinationCube;
    public override void EnterState(StateMachine stateMachine)
    {
        actions = GameObject.Find("ActionsMaster").GetComponent<Actions>();
        target = null;
        destinationCube = null;
        StateTargeting stateTargeting = new StateTargeting();

        target = stateTargeting.FindTarget(stateMachine);

        CubeNeighbors neighbors = target.GetComponent<CombatCharacter>().myCube.GetComponent<CubeNeighbors>();
        List<GameObject> destination = neighbors.FindNeighborCubes(SHAPE.PLUS);
        destinationCube = destination[0];

        CombatSingleton.Instance.CursorCube = destinationCube;
        actions.Movement();
    }

    public override void UpdateState(StateMachine stateMachine)
    {
        if(stateMachine.gameObject.GetComponent<CombatCharacter>().myCube == destinationCube)
        {
            CharacterManipulator.RemoveActionPoints(CombatSingleton.Instance.FocusCharacter, 1);
            stateMachine.TransitionToState(new StatePatrol());
        }
    }
}
