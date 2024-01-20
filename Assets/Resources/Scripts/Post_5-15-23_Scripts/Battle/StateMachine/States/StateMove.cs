using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMove : IState
{
    GameObject target;
    private Actions actions;
    private GameObject destinationCube;
    private bool isDoubleMove;

    public override void EnterState(StateMachine stateMachine)
    {
        isDoubleMove = false;

        MyCondition(stateMachine);


        /*
        actions = GameObject.Find("ActionsMaster").GetComponent<Actions>();
        target = null;
        destinationCube = null;
        StateTargeting stateTargeting = new StateTargeting();

        target = stateTargeting.FindTarget(stateMachine);

        CubeNeighbors neighbors = target.GetComponent<CombatCharacter>().myCube.GetComponent<CubeNeighbors>();
        List<GameObject> destination = neighbors.FindNeighborCubes(SHAPE.PLUS);
        destinationCube = destination[0];*/

        if (destinationCube != null && destinationCube != stateMachine.gameObject.GetComponent<CombatCharacter>().myCube)
        {
            actions = GameObject.Find("ActionsMaster").GetComponent<Actions>();
            CombatSingleton.Instance.CursorCube = destinationCube;
            actions.Movement();
        }

    }

    public override void UpdateState(StateMachine stateMachine)
    {
        if (stateMachine.gameObject.GetComponent<CombatCharacter>().myCube == destinationCube)
        {
            if (isDoubleMove)
            {
                CharacterManipulator.RemoveAllActionPoints(stateMachine.gameObject);
                stateMachine.TransitionToState(new StatePlanning());
                return;
            }
            else
            {
                CharacterManipulator.RemoveActionPoints(CombatSingleton.Instance.FocusCharacter, 1);
                stateMachine.TransitionToState(new StateAttack());
                return;
            }
        }
    }


    private void MyCondition(StateMachine stateMachine)
    {
        destinationCube = null;
        MultiAttack(stateMachine);
        if (destinationCube == null) SingleAttack(stateMachine);
        if (destinationCube == null) ClosestEnemy(stateMachine);
        if (destinationCube == null) NoEnemies(stateMachine);
    }

    private void MultiAttack(StateMachine stateMachine)
    {
        CombatCharacter character = stateMachine.gameObject.GetComponent<CombatCharacter>();
        List<Abilities> abilities = PullCharacterInfo.GenerateMultiTargetDamageAbilityList(character.myStats);
        List<GameObject> enemies = PullOtherCharactersInfo.GetAllEnemies(character);

        if (abilities.Count <= 0) return;
        List<Abilities> canHitAbilities = new List<Abilities>();
        for (int i = 0; i < abilities.Count; i++)
        {
            if(StateCubeInfo.CanIHitMultipleEnemiesIfMoved(abilities[i], enemies, character))
            {
                canHitAbilities.Add(abilities[i]);
            }
        }

        if (canHitAbilities.Count <= 0) return;

        Abilities ability = PullCharacterInfo.MostDamagingAbility(canHitAbilities);

        //Debug.Log("ability Name: " + ability.abilityName);
        destinationCube = StateCubeInfo.FromWhichCubeWouldTheMostBeHit(ability, enemies, character);
    }

    private void SingleAttack(StateMachine stateMachine)
    {
        CombatCharacter character = stateMachine.gameObject.GetComponent<CombatCharacter>();
        List<Abilities> abilities = PullCharacterInfo.GenerateDamageAbilityList(character.myStats);
        List<GameObject> enemies = PullOtherCharactersInfo.GetAllEnemies(character);


        //Debug.Log("character name: " + character.gameObject.name);

        //Debug.Log("abilities count: " + abilities.Count);
        //Debug.Log("enemies count: " + abilities.Count);
        if (abilities.Count <= 0) return;

          List<Abilities> canHitAbilities = new List<Abilities>();
          for (int i = 0; i < abilities.Count; i++)
          {
                for (int x = 0; x < enemies.Count; x++)
            {

                //Debug.Log("enemies count: " + x);
                if (StatePreview.isInRangeIfMoved(enemies[x], abilities[i], character))
                    {
                    //Debug.Log("ability hit");
                        canHitAbilities.Add(abilities[i]);
                        break;
                    }
                }
          }

        //Debug.Log("canHitAbilities count: " + canHitAbilities.Count);
        if (canHitAbilities.Count <= 0) return;

        Abilities ability = PullCharacterInfo.MostDamagingAbility(abilities);

       // Debug.Log("ability Name: " + ability.abilityName);

        enemies = StateCubeInfo.CharactersThatCanBeAttacked(character, ability);
        if (enemies.Count <= 0) return;

        StateTargeting stateTargeting = new StateTargeting();
        GameObject targetEnemy = stateTargeting.BestTargetForAbility(enemies, character);

        //Debug.Log("ability Name: " + ability.abilityName);
        destinationCube = StatePreview.BestSquareToMoveToAttack(targetEnemy, ability, character);
    }

    private void ClosestEnemy(StateMachine stateMachine)
    {
        CombatCharacter character = stateMachine.gameObject.GetComponent<CombatCharacter>();
        List<GameObject> enemies = PullOtherCharactersInfo.GetAllEnemies(character);
        List<GameObject> enemiesWithPath = new List<GameObject>();


        for (int i = 0; i < enemies.Count; i++)
        {
            CubeNeighbors neighbors = enemies[i].GetComponent<CombatCharacter>().myCube.GetComponent<CubeNeighbors>();
            List<GameObject> destination = neighbors.FindNeighborCubes(SHAPE.PLUS);
            List<GameObject> destinationWalkable = new List<GameObject>();

            for (int x = 0; x < destination.Count; x++)
            {
                if(destination[x].GetComponent<CubePhase>().type == GROUNDTYPE.Ground || destination[x] == character.myCube)
                {
                    if (destination[x] != null)
                    {
                        destinationWalkable.Add(destination[x]);
                    }
                }
            }
            if (destinationWalkable.Count > 0)
            {
                for (int x = 0; x < destinationWalkable.Count; x++)
                {
                    destinationCube = destinationWalkable[x];
                    if (StateCubeInfo.IsThereAPath(character, destinationWalkable[x]))
                    {
                        enemiesWithPath.Add(enemies[i]);
                    }
                }
            }
        }
        if (enemiesWithPath.Count <= 0) return;

        GameObject closestCharacter = new GameObject();
        float distance = 10000;

        for (int i = 0; i < enemiesWithPath.Count; i++)
        {
            if (Vector3.Distance(character.gameObject.transform.position, enemiesWithPath[i].transform.position) < distance)
            {
                distance = Vector3.Distance(character.gameObject.transform.position, enemiesWithPath[i].transform.position);
                closestCharacter = enemiesWithPath[i];
            }
        }
        if (character.myStats.actionsRemaining > 1)
        {
            isDoubleMove = true;
            Debug.Log("DoubleMove");
            destinationCube = StateCubeInfo.ClosestCubeDoubleMove(character, closestCharacter);
        }
        else
        {
            Debug.Log("SingleMove");
            destinationCube = StateCubeInfo.ClosestCubeSingleMove(character, closestCharacter);
        }
    }

    private void NoEnemies(StateMachine stateMachine)
    {
        CharacterManipulator.RemoveAllActionPoints(stateMachine.gameObject);
        stateMachine.TransitionToState(new StatePlanning());
        return;
    }

}
