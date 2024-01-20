using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHealAlly : IState
{

    private Actions actions;

    public override void EnterState(StateMachine stateMachine)
    {
        MyCondition(stateMachine);
    }

    public override void UpdateState(StateMachine stateMachine)
    {
        throw new System.NotImplementedException();
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
            foreach (Abilities ability in abilities)
            {
                foreach (GameObject ally in inNeedOfHealing)
                {
                    if (StatePreview.isInRange(ally, ability, character.myCube))
                    {
                        HealAlly(stateMachine, ability, ally);
                        return;
                    }
                }
            }
        }

        JsonRetriever jsonRetriever = new JsonRetriever();
        Item item = jsonRetriever.Load1Item(0);

        if (PullCharacterInfo.DoIHaveAPotion(character.myStats))
        {
            foreach (GameObject ally in allies)
            {
                if (StatePreview.isInRange(ally, 1, character.myCube))
                {
                    HealAlly(stateMachine, item, ally);
                    return;
                }
            }
        }

        if(character.myStats.actionsRemaining > 1)
        {
            stateMachine.TransitionToState(new StateMoveToHeal());
            return;
        }
        else
        {
            stateMachine.TransitionToState(new StateAttack());
            return;
        }
    }

    public void HealAlly(StateMachine stateMachine, Abilities ability, GameObject target)
    {
        CombatSingleton.Instance.actionData.ResetActionData();
        CombatSingleton.Instance.actionData.AiAction = true;
        GameObject.Find("ActionsMaster").GetComponent<Actions>().PreviewAIAbility(ability.id);
        stateMachine.gameObject.GetComponent<CombatCharacter>().myStats.currentMana -= ability.MPCost;

        CombatSingleton.Instance.actionData.TargetCharacters = new List<GameObject>();
        CubeManipulator.ResetAllCubes();

        CombatSingleton.Instance.actionData.TargetCharacters.Add(target);
        CombatSingleton.Instance.actionData.OriginCharacter = stateMachine.gameObject;
        CombatSingleton.Instance.battleSystem.State = BATTLESTATE.ACTION;

        CharacterManipulator.RemoveAllActionPoints(stateMachine.gameObject);
        stateMachine.TransitionToState(new StatePlanning());
        return;
    }

    public void HealAlly(StateMachine stateMachine, Item item, GameObject target)
    {
        CombatSingleton.Instance.actionData.ResetActionData();
        CombatSingleton.Instance.actionData.AiAction = true;
        GameObject.Find("ActionsMaster").GetComponent<Actions>().PreviewAIItem(item.id);

        CombatSingleton.Instance.actionData.TargetCharacters = new List<GameObject>();
        CubeManipulator.ResetAllCubes();

        CombatSingleton.Instance.actionData.TargetCharacters.Add(target);
        CombatSingleton.Instance.actionData.OriginCharacter = stateMachine.gameObject;
        CombatSingleton.Instance.battleSystem.State = BATTLESTATE.ACTION;

        CharacterManipulator.RemoveAllActionPoints(stateMachine.gameObject);
        stateMachine.TransitionToState(new StatePlanning());
        return;
    }











    /*

    public void StateCondition(StateMachine stateMachine)
    {

        List<GameObject> NeedsHealing = DoesAnAllyNeedHealing(stateMachine);
        if (NeedsHealing.Count >0)
        {
            AreAlliesInRange(NeedsHealing, stateMachine);
        }
        else
        {
            stateMachine.TransitionToState(new StateAttack());
        }
    }

    private List<GameObject> DoesAnAllyNeedHealing(StateMachine stateMachine)
    {

        List<GameObject> NeedsHealing = new List<GameObject>();
        foreach (GameObject character in CombatSingleton.Instance.Combatants)
        {
            if(character.GetComponent<CombatCharacter>().team != 0 && stateMachine.gameObject != character)
            {
                Character stats = character.GetComponent<CombatCharacter>().myStats;
                if (((stats.currentHealth / stats.maxHealth)*100) < 30)
                {
                    NeedsHealing.Add(character);
                }
            }
        }
        return NeedsHealing;
    }

    private void AreAlliesInRange(List<GameObject> NeedsHealing, StateMachine stateMachine)
    {
        /*
        foreach(GameObject ally in NeedsHealing)
        {
            if (StatePreview.isInRange())
            {
                HealAlly();
                return;
            }
        }

        if(stateMachine.gameObject.GetComponent<CombatCharacter>().myStats.actionsRemaining > 1)
        {
            foreach (GameObject ally in NeedsHealing)
            {
                if (StatePreview.isInRangeIfMoved())
                {
                    stateMachine.TransitionToState(new StateMoveToHeal());
                }
            }
        }
        else
        {
            stateMachine.TransitionToState(new StateAttack());
        }*/    /*
    }

    private void HealAlly()
    {

    }


    public void EnterState(StateMachine stateMachine, GameObject MoveTo)
    {
        actions = GameObject.Find("ActionsMaster").GetComponent<Actions>();

        CombatSingleton.Instance.CursorCube = MoveTo;
        actions.Movement();
    }*/
}
