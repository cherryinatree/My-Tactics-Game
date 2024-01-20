using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateAttack : IState
{
    private bool isAttacking = false;

    public override void EnterState(StateMachine stateMachine)
    {
        PlanAttack(stateMachine);
    }

    public override void UpdateState(StateMachine stateMachine)
    {

    }


    private void PlanAttack(StateMachine stateMachine)
    {
        if(stateMachine.gameObject.GetComponent<CombatCharacter>().myStats.actionsRemaining > 1)
        {
            stateMachine.TransitionToState(new StateMove());
            return;
        }
        else
        {

           // Debug.Log(1);
            PlanMultiAttack(stateMachine);
            if (stateMachine.gameObject.GetComponent<CombatCharacter>().myStats.actionsRemaining > 0)
            {
                PlanSingleAttack(stateMachine);
                //Debug.Log(2);
            }
           // Debug.Log(3);

            if (stateMachine.gameObject.GetComponent<CombatCharacter>().myStats.actionsRemaining > 0)
            {
                //Debug.Log(4);
                stateMachine.TransitionToState(new StateMove());
                return;
            }
        }



        /*
        if (StatePreview.isInRange())
        {
            Attack(stateMachine, 0);
        }
        else
        {
            stateMachine.TransitionToState(new StateMove());
        }*/
    }

    private void PlanMultiAttack(StateMachine stateMachine)
    {

        CombatCharacter character = stateMachine.gameObject.GetComponent<CombatCharacter>();
        List<Abilities> abilities = PullCharacterInfo.GenerateMultiTargetDamageAbilityList(character.myStats);
        List<GameObject> enemies = PullOtherCharactersInfo.GetAllEnemies(character);

        //Debug.Log("character name: " + character.gameObject.name);

        //Debug.Log("abilities count: " + abilities.Count);
        if (abilities.Count <= 0) return;
        List<Abilities> canHitAbilities = new List<Abilities>();
        for (int i = 0; i < abilities.Count; i++)
        {
            if (StateCubeInfo.CanIHitMultipleEnemiesIfStayStill(abilities[i], enemies, character))
            {
                canHitAbilities.Add(abilities[i]);
            }
        }

        //Debug.Log("canHitAbilities count: " + canHitAbilities.Count);
        if (canHitAbilities.Count <= 0) return;

        Abilities ability = PullCharacterInfo.MostDamagingAbility(canHitAbilities);

        GameObject destinationCube = StateCubeInfo.WhichCubeWouldTheMostEnemiesBeHit(ability, enemies, character.myCube);

        List<GameObject> enemiesHit = StateCubeInfo.CharactersHitAtThisSquareWithMultiTargetAbility(ability, enemies, destinationCube);



        //Debug.Log("First enemy hit: " + enemies[0]);
        CombatSingleton.Instance.actionData.ResetActionData();
        CombatSingleton.Instance.actionData.TargetCharacters = new List<GameObject>();

        for (int i = 0; i < enemiesHit.Count; i++)
        {

            CombatSingleton.Instance.actionData.TargetCharacters.Add(enemiesHit[i]);
        }

        //Debug.Log("ability Name: " + ability.abilityName);
        Attack(stateMachine, ability, destinationCube);
    }

    private void PlanSingleAttack(StateMachine stateMachine)
    {

        CombatCharacter character = stateMachine.gameObject.GetComponent<CombatCharacter>();
        List<Abilities> abilities = PullCharacterInfo.GenerateDamageAbilityList(character.myStats);
        List<GameObject> enemies;

        //Debug.Log("abilities count: " + abilities.Count);
        if (abilities.Count <= 0) return;

        List<Abilities> canHitAbilities = new List<Abilities>();
        for (int i = 0; i < abilities.Count; i++)
        {
            if (StateCubeInfo.CharactersThatCanBeAttacked(character, abilities[i]).Count > 0)
            {
                canHitAbilities.Add(abilities[i]);
            }
        }

        if (canHitAbilities.Count <= 0) return;

        Abilities ability = PullCharacterInfo.MostDamagingAbility(canHitAbilities);
       // Debug.Log("ability Name: " + ability.abilityName);

        enemies = StateCubeInfo.CharactersThatCanBeAttacked(character, ability);
        if (enemies.Count <= 0) return;

        StateTargeting stateTargeting = new StateTargeting();
        GameObject targetEnemy = stateTargeting.BestTargetForAbility(enemies, character);

        GameObject destinationCube = targetEnemy.GetComponent<CombatCharacter>().myCube;
        CombatSingleton.Instance.actionData.ResetActionData();
        CombatSingleton.Instance.actionData.TargetCharacters = new List<GameObject>();
        CombatSingleton.Instance.actionData.TargetCharacters.Add(targetEnemy);
        Attack(stateMachine, ability, destinationCube);
    }



    private void Attack(StateMachine stateMachine, Abilities ability, GameObject cursorCube)
    {

        CombatSingleton.Instance.actionData.AiAction = true;
        GameObject.Find("ActionsMaster").GetComponent<Actions>().PreviewAIAbility(ability.id);
        //StateTargeting stateTargeting = new StateTargeting();
        stateMachine.gameObject.GetComponent<CombatCharacter>().myStats.currentMana -= ability.MPCost;

        CubeManipulator.ResetAllCubes();
        CombatSingleton.Instance.CursorCube = cursorCube;
        //CombatSingleton.Instance.actionData.TargetCharacters.Add(stateTargeting.FindTarget(stateMachine));
        CombatSingleton.Instance.actionData.OriginCharacter = stateMachine.gameObject;
        CombatSingleton.Instance.battleSystem.State = BATTLESTATE.ACTION;
        CharacterManipulator.RemoveAllActionPoints(stateMachine.gameObject);
        stateMachine.TransitionToState(new StatePlanning());
    }
}
