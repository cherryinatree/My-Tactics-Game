using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : IState
{
    public override void EnterState(StateMachine stateMachine)
    {
        Attack(stateMachine, 0);
    }

    public override void UpdateState(StateMachine stateMachine)
    {

    }

    private void Attack(StateMachine stateMachine, int ability)
    {

        CombatSingleton.Instance.actionData.ResetActionData();
        CombatSingleton.Instance.actionData.AiAction = true;
        GameObject.Find("ActionsMaster").GetComponent<Actions>().PreviewAbility(ability);
        CombatSingleton.Instance.actionData.TargetCharacters = new List<GameObject>();
        StateTargeting stateTargeting = new StateTargeting();
        CubeManipulator.ResetAllCubes();
        CombatSingleton.Instance.actionData.TargetCharacters.Add(stateTargeting.FindTarget(stateMachine));
        CombatSingleton.Instance.actionData.OriginCharacter = stateMachine.gameObject;
        CombatSingleton.Instance.battleSystem.State = BATTLESTATE.ACTION;
        CharacterManipulator.RemoveActionPoints(CombatSingleton.Instance.FocusCharacter, 1);
        stateMachine.TransitionToState(new StatePatrol());
    }
}
