using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMoveToHeal : IState
{
    private Actions actions;
    private GameObject destinationCube;


    public override void EnterState(StateMachine stateMachine)
    {
        destinationCube = null;
        MyCondition(stateMachine);
    }

    public override void UpdateState(StateMachine stateMachine)
    {
        if (stateMachine.gameObject.GetComponent<CombatCharacter>().myCube == destinationCube)
        {
            CharacterManipulator.RemoveActionPoints(CombatSingleton.Instance.FocusCharacter, 1);
            stateMachine.TransitionToState(new StateHealAlly());
            return;
        }

        if(destinationCube == null)
        {
            stateMachine.TransitionToState(new StateAttack());
        }
    }


    public void MyCondition(StateMachine stateMachine)
    {
        CombatCharacter character = stateMachine.gameObject.GetComponent<CombatCharacter>();
        List<GameObject> allies = PullOtherCharactersInfo.GetAllAllies(character);
        List<Abilities> abilities = PullCharacterInfo.GenerateHealingAbilityList(character.myStats);
        List<Abilities> HasMana = new List<Abilities>();

        List<GameObject> inNeedOfHealing = new List<GameObject>();


        foreach (GameObject ally in allies)
        {

            float healthPercent =
                ((float)ally.GetComponent<CombatCharacter>().myStats.currentHealth / (float)ally.GetComponent<CombatCharacter>().myStats.maxHealth);

            if ((healthPercent * 100) < 30)
            {
                inNeedOfHealing.Add(ally);
            }
        }

        if (inNeedOfHealing.Count <= 0)
        {
            stateMachine.TransitionToState(new StateAttack());
            return;
        }



        foreach (Abilities ability in abilities)
        {
            if (PullCharacterInfo.HasAbilityAndManaForIt(character.myStats, ability.abilityName))
            {
                if (ability.isSelf == false)
                {
                    HasMana.Add(ability);
                }
            }

        }

        if (HasMana.Count > 0)
        {
            foreach (Abilities ability in HasMana)
            {
                foreach (GameObject ally in inNeedOfHealing)
                {
                    if (StatePreview.isInRangeIfMoved(ally, ability, character))
                    {
                        MoveToHeal(stateMachine, ally);
                        return;
                    }
                }
            }
        }

        if (PullCharacterInfo.DoIHaveAPotion(character.myStats))
        {
            foreach (GameObject ally in allies)
            {
                if (StatePreview.isInRangeIfMoved(ally, 1, character))
                {
                    MoveToHeal(stateMachine, ally);
                    return;
                }
            }
        }
        stateMachine.TransitionToState(new StateAttack());
        return;
    }


    private void MoveToHeal(StateMachine stateMachine, GameObject target)
    {
        GameObject destinationCube = StatePreview.BestSquareToMoveToHeal(target, stateMachine.gameObject.GetComponent<CombatCharacter>());


        actions = GameObject.Find("ActionsMaster").GetComponent<Actions>();

        CombatSingleton.Instance.CursorCube = destinationCube;
        actions.Movement();
    }

}
