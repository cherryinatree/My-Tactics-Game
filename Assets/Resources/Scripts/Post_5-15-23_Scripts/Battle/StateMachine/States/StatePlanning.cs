using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlanning : IState
{

    public override void EnterState(StateMachine stateMachine)
    {


        //PlanTheActions(stateMachine);
    }

    public override void UpdateState(StateMachine stateMachine)
    {
        if (stateMachine.gameObject.GetComponent<CombatCharacter>().myStats.actionsRemaining > 0)
        {
            stateMachine.TransitionToState(new StateHeal());
        }
    }







    private void PlanTheActions(StateMachine stateMachine)
    {
        if (Heal(stateMachine))
        {
            stateMachine.TransitionToState(new StateHeal());
        }
        else if (HealAnAlly(stateMachine))
        {
            stateMachine.TransitionToState(new StateHealAlly());
        }
        else if (MoveToHealAnAlly(stateMachine))
        {
            stateMachine.TransitionToState(new StateMoveToHeal());
        }
        else if (Attack(stateMachine))
        {
            stateMachine.TransitionToState(new StateAttack());
        }
        else if (Move(stateMachine))
        {
            stateMachine.TransitionToState(new StateMove());
        }
        else
        {
            CharacterManipulator.RemoveAllActionPoints(stateMachine.gameObject);
        }
    }
    private bool Heal(StateMachine stateMachine)
    {
        CombatCharacter character = stateMachine.gameObject.GetComponent<CombatCharacter>();
        float healthPercent = (character.myStats.currentHealth / character.myStats.maxHealth);

        if((healthPercent * 100) > 30)
        {
            return false;
        }
        if (PullCharacterInfo.HasAbilityAndManaForIt(character.myStats, "heal"))
        {

            return true;
        }
        if (PullCharacterInfo.HasAbilityAndManaForIt(character.myStats, "self heal"))
        {

            return true;
        }
        if (PullCharacterInfo.DoIHaveAPotion(character.myStats))
        {

            return true;
        }
        return false;
    }

    private bool HealAnAlly(StateMachine stateMachine)
    {

        CombatCharacter character = stateMachine.gameObject.GetComponent<CombatCharacter>();
        List<GameObject> allies = PullOtherCharactersInfo.GetAllAllies(character);
        List<Abilities> abilities = PullCharacterInfo.GenerateHealingAbilityList(character.myStats);
        List<Abilities> HasMana = new List<Abilities>();

        List<GameObject> inNeedOfHealing = new List<GameObject>();

        foreach (GameObject ally in allies)
        {
            float healthPercent =
                (ally.GetComponent<CombatCharacter>().myStats.currentHealth / ally.GetComponent<CombatCharacter>().myStats.maxHealth);

            if((healthPercent * 100) < 30)
            {
                inNeedOfHealing.Add(ally);
            }
        }

        if(inNeedOfHealing.Count <= 0)
        {
            return false;
        }

        foreach (Abilities ability in abilities)
        {
            if (PullCharacterInfo.HasAbilityAndManaForIt(character.myStats, ability.abilityName))
            { 
                HasMana.Add(ability);
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
                        return true;
                    }
                }
            } 
        }
        
        if (PullCharacterInfo.DoIHaveAPotion(character.myStats))
        {
            foreach (GameObject ally in allies)
            {
                if (StatePreview.isInRange(ally, 1, character.myCube))
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool MoveToHealAnAlly(StateMachine stateMachine)
    {
        CombatCharacter character = stateMachine.gameObject.GetComponent<CombatCharacter>();
        List<GameObject> allies = PullOtherCharactersInfo.GetAllAllies(character);
        List<Abilities> abilities = PullCharacterInfo.GenerateHealingAbilityList(character.myStats);
        List<Abilities> HasMana = new List<Abilities>();

        List<GameObject> inNeedOfHealing = new List<GameObject>();


        foreach (GameObject ally in allies)
        {

            float healthPercent =
                (ally.GetComponent<CombatCharacter>().myStats.currentHealth / ally.GetComponent<CombatCharacter>().myStats.maxHealth);

            if ((healthPercent*100) < 30)
            {
                inNeedOfHealing.Add(ally);
            }
        }

        if (inNeedOfHealing.Count <= 0)
        {
            return false;
        }



        foreach (Abilities ability in abilities)
        {
            if (PullCharacterInfo.HasAbilityAndManaForIt(character.myStats, ability.abilityName))
            {
                HasMana.Add(ability);
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
                        return true;
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
                    return true;
                }
            }
        }

        return false;
    }
    private bool Attack(StateMachine stateMachine)
    {
        CombatCharacter character = stateMachine.gameObject.GetComponent<CombatCharacter>();
        List<Abilities> abilities = PullCharacterInfo.GenerateDamageAbilityList(character.myStats);
        List<GameObject> enemies = PullOtherCharactersInfo.GetAllEnemies(character);

        foreach(GameObject enemy in enemies)
        {
            if (StatePreview.isInRangeOfAnyAbility(enemy, abilities, character))
            {
                return true;
            }
        }
        
        return false;
    }
    private bool Move(StateMachine stateMachine)
    {

        CombatCharacter character = stateMachine.gameObject.GetComponent<CombatCharacter>();
        List<GameObject> enemies = PullOtherCharactersInfo.GetAllEnemies(character);

        foreach (GameObject enemy in enemies)
        {
            if (StateCubeInfo.IsThereAPath(character,enemy))
            {
                return true;
            }
        }
        return false;
    }
}
