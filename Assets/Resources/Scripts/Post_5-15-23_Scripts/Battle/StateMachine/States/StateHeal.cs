using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHeal : IState
{
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
        int low = character.myStats.currentHealth;

        // the problem is that the character stats have health as an it so I can do percent. 
        // need to find a work around
        float healthPercent = ((float)character.myStats.currentHealth / (float)character.myStats.maxHealth);

        if ((healthPercent * 100) > 30)
        {
            stateMachine.TransitionToState(new StateHealAlly());
            return;
        }

        if (PullCharacterInfo.HasAbilityAndManaForIt(character.myStats, "self heal"))
        {
            Abilities ability = PullCharacterInfo.GetAbility(character.myStats, "self heal");
            HealMe(stateMachine, ability);
        }

        if (PullCharacterInfo.DoIHaveAPotion(character.myStats))
        {
            JsonRetriever jsonRetriever = new JsonRetriever();
            Item item = jsonRetriever.Load1Item(0);

            HealMeWithPotion(stateMachine, item);
            return;
        }
        if (PullCharacterInfo.HasAbilityAndManaForIt(character.myStats, "heal"))
        {

            Abilities ability = PullCharacterInfo.GetAbility(character.myStats, "heal");
            HealMe(stateMachine, ability);
            return;
        }

        stateMachine.TransitionToState(new StateAttack());
        return;
    }

    private void HealMe(StateMachine stateMachine, Abilities ability)
    {
        CombatSingleton.Instance.actionData.ResetActionData();
        CombatSingleton.Instance.actionData.AiAction = true;
        GameObject.Find("ActionsMaster").GetComponent<Actions>().PreviewAIAbility(ability.id);
        stateMachine.gameObject.GetComponent<CombatCharacter>().myStats.currentMana -= ability.MPCost;

        CombatSingleton.Instance.actionData.TargetCharacters = new List<GameObject>();
        CubeManipulator.ResetAllCubes();

        CombatSingleton.Instance.actionData.TargetCharacters.Add(stateMachine.gameObject);
        CombatSingleton.Instance.actionData.OriginCharacter = stateMachine.gameObject;
        CombatSingleton.Instance.battleSystem.State = BATTLESTATE.ACTION;

        CharacterManipulator.RemoveAllActionPoints(stateMachine.gameObject);
        stateMachine.TransitionToState(new StatePlanning());
        return;
    }

    private void HealMeWithPotion(StateMachine stateMachine, Item item)
    {
        CombatSingleton.Instance.actionData.ResetActionData();
        CombatSingleton.Instance.actionData.AiAction = true;
        GameObject.Find("ActionsMaster").GetComponent<Actions>().PreviewAIItem(item.id);

        CombatSingleton.Instance.actionData.TargetCharacters = new List<GameObject>();
        CubeManipulator.ResetAllCubes();

        CombatSingleton.Instance.actionData.TargetCharacters.Add(stateMachine.gameObject);
        CombatSingleton.Instance.actionData.OriginCharacter = stateMachine.gameObject;
        CombatSingleton.Instance.battleSystem.State = BATTLESTATE.ACTION;

        CharacterManipulator.RemoveAllActionPoints(stateMachine.gameObject);
        stateMachine.TransitionToState(new StatePlanning());
        return;
    }

    /*
    public void StateCondition(StateMachine stateMachine)
    {
        Character stats = stateMachine.gameObject.GetComponent<CombatCharacter>().myStats;
        if (PullCharacterInfo.HasAbilityAndManaForIt(stats, "self heal"))
        {
            Heal(stateMachine, stats);
        }
        else
        {
            stateMachine.TransitionToState(new StateHealAlly());
        }
    }

    private void Heal(StateMachine stateMachine, Character stats)
    {
        if (((stats.currentHealth / stats.maxHealth)*100) < 30)
        {
            Abilities ability = PullCharacterInfo.GetAbility(stats, "self heal");
            CombatSingleton.Instance.actionData.ResetActionData();
            CombatSingleton.Instance.actionData.AiAction = true;
            GameObject.Find("ActionsMaster").GetComponent<Actions>().PreviewAbility(ability.id);
            CombatSingleton.Instance.actionData.TargetCharacters = new List<GameObject>();
            CubeManipulator.ResetAllCubes();
            CombatSingleton.Instance.actionData.TargetCharacters.Add(stateMachine.gameObject);
            CombatSingleton.Instance.actionData.OriginCharacter = stateMachine.gameObject;
            CombatSingleton.Instance.battleSystem.State = BATTLESTATE.ACTION;
            CharacterManipulator.RemoveAllActionPoints(CombatSingleton.Instance.FocusCharacter);
            stateMachine.TransitionToState(new StatePlanning());
        }
        else
        {
            stateMachine.TransitionToState(new StateHealAlly());
        }
    }
    */
}
