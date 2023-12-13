using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionMath
{
    static int hitChance = 75;
    
    public static bool DoesItHit(Abilities ability, GameObject Target)
    {
        if (ability.isFriendly)
        {
            return true;
        }

        Character originCharacter = CombatSingleton.Instance.actionData.OriginCharacter.GetComponent<CombatCharacter>().myStats;
        Character TargetCharacter = Target.GetComponent<CombatCharacter>().myStats;
        
        int hitChanceModify = 0;
        if (ability.MPCost > 0)
        {
            hitChanceModify += (originCharacter.intelligence - TargetCharacter.defense)*2;
        }
        else
        {
            hitChanceModify += (originCharacter.attack - TargetCharacter.defense)*2;
        }

        hitChanceModify += (originCharacter.level - TargetCharacter.level)*3;

        if((hitChance+ hitChanceModify) > Random.Range(0, 100))
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public static void CalulateDamage(Abilities ability, GameObject Target)
    {
        Character originStats = CombatSingleton.Instance.actionData.OriginCharacter.GetComponent<CombatCharacter>().myStats;
        Character targetStats = Target.GetComponent<CombatCharacter>().myStats;

        int damage = Random.Range(ability.minEffect, ability.maxEffect) + ((originStats.intelligence - targetStats.defense)) +
            ((originStats.level - targetStats.level));

        if(damage < 1)
        {
            damage = 1;
        }

        targetStats.currentHealth -= damage;
        if(targetStats.currentHealth < 0)
        {
            targetStats.currentHealth = 0;
        }
    }

    public static void CalulateBenift(Abilities ability, GameObject Target)
    {
        Character originStats = CombatSingleton.Instance.actionData.OriginCharacter.GetComponent<CombatCharacter>().myStats;
        Character targetStats = Target.GetComponent<CombatCharacter>().myStats;


        switch (ability.effect)
        {
            case ("heal"):
                AbilityEffects.Heal(ability, targetStats, originStats);
                break;
        }
    }
    public static void CalulateBenift(Item item, GameObject Target)
    {
        Character originStats = CombatSingleton.Instance.actionData.OriginCharacter.GetComponent<CombatCharacter>().myStats;
        Character targetStats = Target.GetComponent<CombatCharacter>().myStats;


        switch (item.type)
        {
            case ("heal"):
                ItemEffects.Heal(item, targetStats);
                break;
        }
    }

    private static bool DoesItCapture(GameObject target)
    {


        Character originCharacter = CombatSingleton.Instance.actionData.OriginCharacter.GetComponent<CombatCharacter>().myStats;
        Character TargetCharacter = target.GetComponent<CombatCharacter>().myStats;


        hitChance = (100 - ((TargetCharacter.currentHealth / TargetCharacter.maxHealth) * 100));

        int hitChanceModify = 0;
        hitChanceModify += (originCharacter.intelligence - TargetCharacter.defense) * 2;
        hitChanceModify += (originCharacter.level - TargetCharacter.level) * 3;

        if ((hitChance + hitChanceModify) >= Random.Range(0, 100))
        {
            return true;
        }
        else
        {
            return false;
        }

    }


    public static List<bool> CheckIfCaptures()
    {
        List<bool> result = new List<bool>();
        foreach (GameObject target in CombatSingleton.Instance.actionData.TargetCharacters)
        {
            result.Add(DoesItCapture(target));
        }
        return result;
    }

    public static List<bool> CheckIfHits()
    {
        List<bool> result = new List<bool>();
        foreach (GameObject target in CombatSingleton.Instance.actionData.TargetCharacters)
        {
            result.Add(DoesItHit(CombatSingleton.Instance.actionData.ChosenAbility, target));
        }
        return result;
    }
    public static List<bool> AlwaysHits()
    {
        List<bool> result = new List<bool>();
        foreach (GameObject target in CombatSingleton.Instance.actionData.TargetCharacters)
        {
            result.Add(true);
        }
        return result;
    }

}
