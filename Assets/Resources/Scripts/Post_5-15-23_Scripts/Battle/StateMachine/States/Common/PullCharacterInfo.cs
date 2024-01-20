using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PullCharacterInfo 
{


    public static bool DoIHaveAPotion(Character character)
    {
        if(character.item0 != -1)
        {
            return true;
        }else if(character.item1 != -1)
        {
            return true;
        }else if(character.item2 != -1)
        {
            return true;
        }
        else { return false; }
    }

    public static Abilities GetAbility(Character character, string abilityName)
    {
        List<Abilities> abilities = GenerateAbilityList(character);
        foreach (Abilities ability in abilities)
        {
            if (ability.effect == abilityName || ability.abilityName == abilityName)
            {
                if (character.currentMana >= ability.MPCost)
                {
                    return ability;
                }
            }
        }
        return null;
    }

    public static bool HasAbilityAndManaForIt(Character character, string abilityName)
    {
        List<Abilities> abilities = GenerateAbilityList(character);
        foreach (Abilities ability in abilities)
        {
            if(ability.effect == abilityName || ability.abilityName == abilityName)
            {
                if(character.currentMana >= ability.MPCost)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static List<Abilities> GenerateAbilityList(Character character)
    {
        JsonRetriever jsonRetriever = new JsonRetriever();
        List<Abilities> result = new List<Abilities>();
        for (int i = 0; i < character.Abilities.Length; i++)
        {
            if (character.AquiredAbilites[i])
            {
                Abilities ability = jsonRetriever.Load1Ability(character.Abilities[i]);
                if (character.currentMana >= ability.MPCost)
                {
                    result.Add(ability);
                }
            }
        }

        return result;
    }
    public static List<Abilities> GenerateDamageAbilityList(Character character)
    {
        JsonRetriever jsonRetriever = new JsonRetriever();
        List<Abilities> result = new List<Abilities>();
        for (int i = 0; i < character.Abilities.Length; i++)
        {
            if (character.AquiredAbilites[i])
            {
                Abilities ability = jsonRetriever.Load1Ability(character.Abilities[i]);
                if (!ability.isFriendly && ability.isSingleTarget)
                {
                    if (character.currentMana >= ability.MPCost)
                    {
                        result.Add(ability);
                    }
                }
            }
        }

        return result;
    }
    public static List<Abilities> GenerateMultiTargetDamageAbilityList(Character character)
    {
        JsonRetriever jsonRetriever = new JsonRetriever();
        List<Abilities> result = new List<Abilities>();
        for (int i = 0; i < character.Abilities.Length; i++)
        {
            if (character.AquiredAbilites[i])
            {
                Abilities ability = jsonRetriever.Load1Ability(character.Abilities[i]);
                if (!ability.isFriendly && !ability.isSingleTarget)
                {
                    result.Add(ability);
                }
            }
        }

        return result;
    }
    public static List<Abilities> GenerateHealingAbilityList(Character character)
    {
        JsonRetriever jsonRetriever = new JsonRetriever();
        List<Abilities> result = new List<Abilities>();
        for (int i = 0; i < character.Abilities.Length; i++)
        {
            if (character.AquiredAbilites[i])
            {
                Abilities ability = jsonRetriever.Load1Ability(character.Abilities[i]);
                if (ability.isFriendly)
                {
                    if (character.currentMana >= ability.MPCost)
                    {
                        result.Add(ability);
                    }
                }
            }
        }

        return result;
    }

    public static Abilities MostDamagingAbility(List<Abilities> abilities)
    {
        float highestDamage = 0;
        Abilities HighestDamageAbility = abilities[0];

        for (int i = 0; i < abilities.Count; i++)
        {

            if ((abilities[i].minEffect + abilities[i].maxEffect) > highestDamage)
            {
                highestDamage = (abilities[i].minEffect + abilities[i].maxEffect);
                HighestDamageAbility = abilities[i];
            }
        }

        return HighestDamageAbility;
    }
}
