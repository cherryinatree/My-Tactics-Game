using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterInfoRetriever
{
    
    public static List<int> GetListOfItems(Character stats)
    {
        List<int> items = new List<int>();

        items.Add(stats.item0);
        items.Add(stats.item1);
        items.Add(stats.item2);

        return items;
    }

    public static List<int> GetListOfEquipment(Character stats)
    {
        List<int> equipment = new List<int>();
        JsonRetriever jsonRetriever = new JsonRetriever();

        equipment.Add(stats.weapon);
        equipment.Add(stats.offHand);
        equipment.Add(stats.helm);
        equipment.Add(stats.armor);
        equipment.Add(stats.boots);
        equipment.Add(stats.amulet);

        return equipment;
    }

    public static List<Abilities> GetAbilities(Character stats)
    {
        List<Abilities> abilities = new List<Abilities>();
        JsonRetriever jsonRetriever = new JsonRetriever();
        
        foreach (int ability in stats.Abilities)
        {
            abilities.Add(jsonRetriever.Load1Ability(ability));
        }
        return abilities;
    }

    public static bool DoesCharacterOwnAbility(Character stats, Abilities ability)
    {
        for (int i = 0; i < stats.Abilities.Length; i++)
        {
            if (stats.Abilities[i] == ability.id)
            {
                if (stats.AquiredAbilites[i] == true)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
